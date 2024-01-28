using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventTimeline.DTOs;

namespace Vennderful.Application.Features.EventTimeline.Responses
{
    public class GetEventTimelinesResponse : BaseResponse
    {
        public List<EventTimelineDto> Data { get; set; }
    }
}
