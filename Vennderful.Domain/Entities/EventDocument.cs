using System;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class EventDocument : BaseAuditableEntity
    {
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public DocumentSignerType DocumentSignerType { get; set; }
    }
}
