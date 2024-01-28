using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventPayment.DTOs;
using Vennderful.Application.Features.EventPayment.Requests;
using Vennderful.Application.Features.EventPayment.Responses;

namespace Vennderful.Application.Features.EventPayment.Handlers.Queries
{
    public class GetEventPaymentRequestHandler : IRequestHandler<GetEventPaymentByClientIdRequest, GetEventPaymentByClientIdResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventPaymentRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetEventPaymentByClientIdResponse> Handle(GetEventPaymentByClientIdRequest request, CancellationToken cancellationToken)
        {
            var eventPayment = await _unitOfWork.eventPaymentRepository.GetEventPaymentByClientId(request.ClientId,request.EventId);
            var response = new GetEventPaymentByClientIdResponse();
            if (eventPayment == null)
            {
                response.Success = false;
                response.Message = "Event Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = _mapper.Map<EventPaymentAmountDTO>(eventPayment);
            return response;
        }
    }
}
