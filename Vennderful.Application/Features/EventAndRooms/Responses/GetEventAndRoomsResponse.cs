using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventAndRooms.Dto;

namespace Vennderful.Application.Features.EventAndRooms.Responses
{
    public class GetEventAndRoomsResponse: BaseResponse
    {
        public List<ListEventAndRoomDto> Data { get; set; }
    }
}
