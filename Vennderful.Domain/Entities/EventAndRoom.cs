using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class EventAndRoom : BaseAuditableEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid CompanyId { get; set; }
        public List<Guid> RoomId { get; set; }
        public List<Room> Rooms { get; set; }

    }


}
