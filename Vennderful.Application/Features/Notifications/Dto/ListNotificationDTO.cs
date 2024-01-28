using System;
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.Notifications.Dto
{
    public class ListNotificationDTO : BaseDTO
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
    }
}
