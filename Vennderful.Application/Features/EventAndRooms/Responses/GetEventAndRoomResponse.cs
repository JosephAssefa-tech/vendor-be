using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventAndRooms.Dto;

namespace Vennderful.Application.Features.EventAndRooms.Responses
{
    public class GetEventAndRoomResponse : BaseResponse
    {
        public EventAndRoomDto Data { get; set; }
    }
}
