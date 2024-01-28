using System;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class EventClient: BaseAuditableEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public string? Note { get; set; }
        public InvitationStatus Status { get; set; }
    }
}
