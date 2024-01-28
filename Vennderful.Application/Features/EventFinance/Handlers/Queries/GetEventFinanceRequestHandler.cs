using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.EventFinance.Requests;
using Vennderful.Application.Features.EventFinance.Responses;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using MediatR;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Domain.Entities;
using System.Collections.Generic;

namespace Vennderful.Application.Features.EventFinance.Handlers.Queries
{
    public class GetEventFinanceRequestHandler : IRequestHandler<GetEventFinanceRequest, GetEventFinanceResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventFinanceRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetEventFinanceResponse> Handle(GetEventFinanceRequest request, CancellationToken cancellationToken)
        {
            var eventFinance = await _unitOfWork.eventFinanceRepository.GetEventFinanceByEventId(request.EventId);
            var response = new GetEventFinanceResponse();
            if (eventFinance == null)
            {
                response.Success = false;
                response.Message = "The event could not be found.";
                return response;
            }
            var eventFinanceDto = _mapper.Map<EventFinanceDto>(eventFinance);
            var eventFinanceAddOn = await _unitOfWork.eventFinanceAddOnRepository.GetEventFinanceAddOnByEventFinanceId(eventFinance.Id);
            eventFinanceDto.Addons = _mapper.Map<List<AddonDto>>(eventFinanceAddOn);
            var eventFinancePaymentSchedule = await _unitOfWork.eventFinancePaymentScheduleRepository.GetEventFinancePaymentScheduleByEventFinanceId(eventFinance.Id);
            eventFinanceDto.Payments = _mapper.Map<List<PaymentSchedules>>(eventFinancePaymentSchedule);

            response.Success = true;
            response.Data = eventFinanceDto;
            return response;
        }
    }
}
