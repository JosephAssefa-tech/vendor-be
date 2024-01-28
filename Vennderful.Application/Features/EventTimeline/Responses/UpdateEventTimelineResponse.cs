using Vennderful.Application.Common;
using Vennderful.Application.Features.EventTimeline.DTOs;

namespace Vennderful.Application.Features.EventTimeline.Responses
{
    public class UpdateEventTimelineResponse : BaseResponse
    {
        public UpdateEventTimelineDto Data { get; set; }
    }
}
