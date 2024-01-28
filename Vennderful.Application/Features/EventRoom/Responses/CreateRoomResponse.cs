using Vennderful.Application.Common;
using Vennderful.Application.Features.EventRoom.Dto;

namespace Vennderful.Application.Features.EventRoom.Responses
{
    public class CreateRoomResponse: BaseResponse
    {
        public CreateRoomDto Data { get; set; }
    }
}
