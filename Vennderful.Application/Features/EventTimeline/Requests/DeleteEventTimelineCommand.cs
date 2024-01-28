using MediatR;
using System;
using Vennderful.Application.Features.EventTimeline.Responses;

namespace Vennderful.Application.Features.EventTimeline.Requests
{
    public class DeleteEventTimelineCommand : IRequest<DeleteEventTimelineResponse>
    {
        public Guid Id { get; set; }
    }
}
