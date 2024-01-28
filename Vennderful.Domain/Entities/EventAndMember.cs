using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class EventAndMember : BaseAuditableEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public bool IsActive { get; set; }
    }
}
