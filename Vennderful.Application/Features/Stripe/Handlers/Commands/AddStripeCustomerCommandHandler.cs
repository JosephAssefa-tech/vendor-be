using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Payment;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Stripe.DTOs;
using Vennderful.Application.Features.Stripe.Requests;
using Vennderful.Application.Features.Stripe.Responses;
using Vennderful.Application.Features.Stripe.Validators;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.Stripe.Handlers.Commands
{
    public class AddStripeCustomerCommandHandler : IRequestHandler<AddStripeCustomerCommand, AddStripeCustomerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentServices _paymentServices;

        public AddStripeCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPaymentServices paymentServices) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentServices = paymentServices;
        }

        public async Task<AddStripeCustomerResponse> Handle(AddStripeCustomerCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddStripeCustomerDTOValidator();
            var validationResult = await validator.ValidateAsync(request.AddStripeCustomerDTO);

            var response = new AddStripeCustomerResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var customer = await _paymentServices.AddStripeCustomerAsync(request.AddStripeCustomerDTO, cancellationToken);

            if (customer.Id != null)
            {
                var stripeCustomer = _mapper.Map<Customer>(request.AddStripeCustomerDTO);
                stripeCustomer = await _unitOfWork.CustomerRepository.AddAsync(stripeCustomer);

                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Created Successfully.";
                response.Data = _mapper.Map<AddStripeCustomerDTO>(stripeCustomer);
            }
            else
            {
                response.Success = false;
                response.Message = "Creating customer failed.";
            }
            
            return response;
        }
    }
}
