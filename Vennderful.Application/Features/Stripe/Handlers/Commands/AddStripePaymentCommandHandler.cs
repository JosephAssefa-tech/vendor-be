using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Stripe.DTOs;
using Vennderful.Application.Features.Stripe.Requests;
using Vennderful.Application.Features.Stripe.Responses;
using Vennderful.Application.Features.Stripe.Validators;
using Vennderful.Domain.Entities;
using FluentValidation;
using System.Linq;
using Vennderful.Application.Contracts.Payment;

namespace Vennderful.Application.Features.Stripe.Handlers.Commands
{
    public class AddStripePaymentCommandHandler : IRequestHandler<AddStripePaymentCommand, AddStripePaymentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentServices _paymentServices;

        public AddStripePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPaymentServices paymentServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentServices = paymentServices;
        }

        public async Task<AddStripePaymentResponse> Handle(AddStripePaymentCommand request, CancellationToken cancellationToken)
        {
            var validator = new AddStripePaymentDTOValidator();
            var validationResult = await validator.ValidateAsync(request.AddStripePaymentDTO);

            var response = new AddStripePaymentResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var payment = await _paymentServices.AddStripePaymentAsync(request.AddStripePaymentDTO, cancellationToken);

            if (payment.Id != null)
            {
                var stripePayment = _mapper.Map<Payment>(request.AddStripePaymentDTO);
                stripePayment = await _unitOfWork.PaymentRepository.AddAsync(stripePayment);

                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Payment Successful.";
                response.Data = _mapper.Map<AddStripePaymentDTO>(stripePayment);
            } else
            {
                response.Success = false;
                response.Message = "Payment failed.";
            }

            return response;
        }
    }
}
