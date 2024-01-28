using MediatR;
using System;
using Vennderful.Application.Features.EventPayment.Responses;

namespace Vennderful.Application.Features.EventPayment.Requests
{
    public class GetEventPaymentsRequest : IRequest<GetEventPaymentsResponse>
    {
        public Guid EventId { get; set; }
    }
}
