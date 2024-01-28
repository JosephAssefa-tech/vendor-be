using Vennderful.Application.Common;
using Vennderful.Application.Features.EventTimeline.DTOs;

namespace Vennderful.Application.Features.EventTimeline.Responses
{
    public class CreateEventTimelineResponse : BaseResponse
    {
        public CreateEventTimelineDto Data { get; set; }
    }
}
