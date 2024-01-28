using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Vennderful.Application.Features.EventPayment.Requests;
using Vennderful.Application.Features.EventPayment.Responses;
using Vennderful.Application.Contracts.Persitence;
using AutoMapper;
using System.Linq;
using Vennderful.Application.Features.EventPayment.DTOs;

namespace Vennderful.Application.Features.EventPayment.Handlers.Queries
{
    public class GetEventPaymentsRequestHandler : IRequestHandler<GetEventPaymentsRequest, GetEventPaymentsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventPaymentsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetEventPaymentsResponse> Handle(GetEventPaymentsRequest request,
            CancellationToken cancellationToken)
        {
            var response = new GetEventPaymentsResponse();
            try
            {
                var eventPayments = (await _unitOfWork.eventPaymentRepository.GetEventPaymentsByEventId(request.EventId)).ToList();

                response.Success = true;
                response.Data = _mapper.Map<List<EventPaymentDTO>>(eventPayments);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong.";
                response.Data = new List<EventPaymentDTO>();
                response.Errors = new List<string>() { ex.Message };

                return response;
            }
        }
    }
}
