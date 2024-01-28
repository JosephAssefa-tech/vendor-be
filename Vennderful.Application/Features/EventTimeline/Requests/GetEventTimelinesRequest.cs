using MediatR;
using System;
using Vennderful.Application.Features.EventTimeline.Responses;

namespace Vennderful.Application.Features.EventTimeline.Requests
{
    public class GetEventTimelinesRequest : IRequest<GetEventTimelinesResponse>
    {
        public Guid EventId { get; set; }
    }
}
