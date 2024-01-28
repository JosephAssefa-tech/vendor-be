using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventRoom.Dto;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.EventAndRooms.Dto
{
    public class CreateEventAndRoomDto: BaseDTO
    {
        public Guid EventId { get; set; }
        public List<Guid> RoomId { get; set; }
        public Guid CompanyId { get; set; }



    }
}
