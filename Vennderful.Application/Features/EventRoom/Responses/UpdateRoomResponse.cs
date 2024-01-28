using Vennderful.Application.Common;
using Vennderful.Application.Features.EventRoom.Dto;

namespace Vennderful.Application.Features.EventRoom.Responses
{
    public class UpdateRoomResponse : BaseResponse
    {
        public UpdateRoomDto Data { get; set; }
    }
}
