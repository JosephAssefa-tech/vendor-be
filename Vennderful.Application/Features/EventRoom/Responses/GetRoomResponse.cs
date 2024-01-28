using Vennderful.Application.Common;
using Vennderful.Application.Features.EventRoom.Dto;

namespace Vennderful.Application.Features.EventRoom.Responses
{
    public class GetRoomResponse : BaseResponse
    {
        public RoomDto Data { get; set; }
    }
}
