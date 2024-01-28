using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.EventAndRooms.Dto
{
    public class ListEventAndRoomDto: BaseDTO
    {
        public Guid EventId { get; set; }
        public List<Guid> RoomId { get; set; }
    }
}
