using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;

namespace Vennderful.Application.Features.Events.Responses
{
    public class GetEventClentsResponse : BaseResponse
    {
        public List<ListClientDTO> Data { get; set; }
    }
}
