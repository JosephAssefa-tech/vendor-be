using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventAndRooms.Dto;
using Vennderful.Application.Features.EventAndRooms.Responses;

namespace Vennderful.Application.Features.EventAndRooms.Requests
{
    public class CreateEventAndRoomsCommand: IRequest<CreateEventAndRoomResponse>
    {
        public CreateEventAndRoomDto CreateEventAndRoomDto { get; set; }
    }
}
