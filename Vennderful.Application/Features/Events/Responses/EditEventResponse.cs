using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.Events.Responses
{
    public class EditEventResponse : BaseResponse
    {
        public EditEventDTO Data { get; set; }
    }
}
