using System;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class Notification : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public NotificationType NotificationType { get; set; }
        public NotificationMethod NotificationMethod { get; set; }
        public string Content { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? EventId { get; set; }
        public Guid? EventDocumentId { get; set; }
        public Guid? DocumentId { get; set; }
        public Guid? SenderId { get; set; }
        public bool HasBeenRead { get; set; }
    }
}
