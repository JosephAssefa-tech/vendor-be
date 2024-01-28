using MediatR;
using Vennderful.Application.Features.EventTimeline.DTOs;
using Vennderful.Application.Features.EventTimeline.Responses;

namespace Vennderful.Application.Features.EventTimeline.Requests
{
    public class UpdateEventTimelineCommand : IRequest<UpdateEventTimelineResponse>
    {
        public UpdateEventTimelineDto UpdateEventTimelineDto { get; set; }
    }
}
