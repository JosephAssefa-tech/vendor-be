using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventRoom.Dto;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.EventAndRooms.Dto
{
    public class EventAndRoomDto: BaseDTO
    {
        public Guid EventId { get; set; }
        public EventDTO Event { get; set; }
        public RoomDto Room { get; set; }
        public Guid CompanyId { get; set; }
        public Guid RoomId { get; set; }



    }
}
