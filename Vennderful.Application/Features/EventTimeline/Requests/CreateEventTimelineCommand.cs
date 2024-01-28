using MediatR;
using System;
using Vennderful.Application.Features.EventTimeline.DTOs;
using Vennderful.Application.Features.EventTimeline.Responses;

namespace Vennderful.Application.Features.EventTimeline.Requests
{
    public class CreateEventTimelineCommand : IRequest<CreateEventTimelineResponse>
    {
        public Guid EventId { get; set; }
        public CreateEventTimelineDto CreateEventTimelineDto { get; set; }
    }
}
