using System;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class EventDocumentSigner: BaseAuditableEntity
    {
        public Guid EventDocumentId { get; set; }
        public EventDocument EventDocument { get; set; }
        public  Guid SignerId { get; set; }
        public Guid SignatureRequestSender { get; set; }
        public DocumentStatus DocumentStatus { get; set; }

       
    }
}
