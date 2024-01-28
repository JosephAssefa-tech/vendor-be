using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.EventPayment.Requests;
using Vennderful.Application.Features.EventPayment.Responses;
using MediatR;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.EventPayment.Validators;
using System.Linq;
using Vennderful.Application.Features.EventPayment.DTOs;
using System;

namespace Vennderful.Application.Features.EventPayment.Handlers.Commands
{
    public class CreateEventPaymentCommandHandler : IRequestHandler<CreateEventPaymentCommand, CreateEventPaymentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEventPaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateEventPaymentResponse> Handle(CreateEventPaymentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventPaymentValidator();
            var validationResult = await validator.ValidateAsync(request.CreateEventPaymentDTO);

            var response = new CreateEventPaymentResponse();

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }
            var eventPayment = _mapper.Map<Vennderful.Domain.Entities.EventPayment>(request.CreateEventPaymentDTO);
            eventPayment = await _unitOfWork.eventPaymentRepository.AddAsync(eventPayment);

            if(eventPayment != null && eventPayment.EventFinancePaymentScheduleId != null)
            {
                var payment = await _unitOfWork.eventFinancePaymentScheduleRepository.GetEventFinancePaymentScheduleById((int)eventPayment.EventFinancePaymentScheduleId);
                if(payment != null)
                {
                    payment.Status = Domain.Enums.PaymentStatus.Paid;
                    await _unitOfWork.eventFinancePaymentScheduleRepository.UpdateAsync(payment);
                }
            }
                await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateEventPaymentDTO>(eventPayment);
            return response;
        }
    }
}
