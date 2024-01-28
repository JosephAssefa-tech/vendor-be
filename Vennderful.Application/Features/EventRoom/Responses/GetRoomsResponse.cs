using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventRoom.Dto;

namespace Vennderful.Application.Features.EventRoom.Responses
{
    public class GetRoomsResponse : BaseResponse
    {
        public List<ListRoomDto> Data { get; set; }
    }
}
