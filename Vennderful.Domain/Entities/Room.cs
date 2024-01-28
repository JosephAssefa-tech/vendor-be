using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Room: BaseAuditableEntity
    {
        public string RoomName { get; set; }
        public Guid CompanyId { get; set; }
        public IList<EventAndRoom> EventRooms { get; set; }
        //public virtual IList<Event> Events { get; set; }

    }
}
