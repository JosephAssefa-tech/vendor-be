using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventDocuments.Dto;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.EventDocumentSignature.Dto
{
    public class EventDocumentSignatureDto : BaseDTO
    {
        public Guid EventDocumentId { get; set; }
        public Guid SignerId { get; set; }
        public Guid SignatureRequestSender { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
    }
}
