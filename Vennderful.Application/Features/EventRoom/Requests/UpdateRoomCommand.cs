using MediatR;
using Vennderful.Application.Features.EventRoom.Responses;
using Vennderful.Application.Features.EventRoom.Dto;

namespace Vennderful.Application.Features.EventRoom.Requests
{
    public class UpdateRoomCommand : IRequest<UpdateRoomResponse>
    {
        public UpdateRoomDto UpdateRoomDto{ get; set; }
    }
}
