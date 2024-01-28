using MediatR;
using System;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Requests
{
    public class GetEventRequest : IRequest<GetEventResponse>
    {
        public string EventId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
