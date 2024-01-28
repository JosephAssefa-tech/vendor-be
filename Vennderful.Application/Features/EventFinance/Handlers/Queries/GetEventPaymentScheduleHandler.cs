using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.EventFinance.Requests;
using Vennderful.Application.Features.EventFinance.Responses;
using MediatR;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using Vennderful.Application.Features.EventFinance.Dto;
using System.Linq;

namespace Vennderful.Application.Features.EventFinance.Handlers.Queries
{
    public class GetEventPaymentScheduleHandler : IRequestHandler<GetEventPaymentSchedulesRequest, GetEventPaymentSchedulesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventPaymentScheduleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetEventPaymentSchedulesResponse> Handle(GetEventPaymentSchedulesRequest request, CancellationToken cancellationToken)
        {
            var eventFinance = await _unitOfWork.eventFinanceRepository.GetEventFinanceByEventId(request.EventId);
            var response = new GetEventPaymentSchedulesResponse();
            if (eventFinance == null)
            {
                response.Success = false;
                response.Message = "The event could not be found.";
                return response;
            }
            var scheduledPayment = await _unitOfWork.eventFinancePaymentScheduleRepository.GetEventFinancePaymentScheduleByEventFinanceIdAndStatus(eventFinance.Id);
            if(scheduledPayment != null)
            {
                List<ListEventPaymentScheduleDto> payments = new List<ListEventPaymentScheduleDto>();
                if (eventFinance.DepositStatus != Domain.Enums.PaymentStatus.Paid)
                {
                    payments.Add(new ListEventPaymentScheduleDto
                    {
                        EventFinanceId = eventFinance.Id,
                        PaymentDate = eventFinance.DepositDueDate,
                        ScheduleAmount = eventFinance.DepositAmount,
                        Id = 0,
                        Status = eventFinance.DepositStatus.ToString(),
                        PaymentTitle = "Payment #1 (Deposit)",
                });
                }
                payments.AddRange(_mapper.Map<List<ListEventPaymentScheduleDto>>(scheduledPayment));

                response.Success = true;
                response.Message = "";
                response.Data = payments;

            }
            return response;
        }
    }
}
