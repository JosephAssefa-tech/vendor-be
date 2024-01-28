using MediatR;
using System;
using Vennderful.Application.Features.EventFinance.Responses;

namespace Vennderful.Application.Features.EventFinance.Requests
{
    public class GetEventPaymentSchedulesRequest : IRequest<GetEventPaymentSchedulesResponse>
    {
        public Guid EventId { get; set; }
    }
}
