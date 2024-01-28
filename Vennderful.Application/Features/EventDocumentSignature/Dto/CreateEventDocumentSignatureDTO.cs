using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.EventDocumentSignature.Dto
{
    public class CreateEventDocumentSignatureDTO : BaseDTO
    {
        public Guid EventDocumentId { get; set; }  
        public List<Guid> SignerId { get; set; }
        public Guid SignatureRequestSender { get; set; }
  
    }
}
