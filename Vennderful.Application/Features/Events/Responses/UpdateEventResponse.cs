using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.Events.Responses
{
    public class UpdateEventResponse: BaseResponse
    {
        public UpdateEventDto Data { get; set; }
    }
}
