using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.EventDocuments.Dto
{
    public class EventDocumentsDto : BaseDTO
    {
        // public Guid EventId { get; set; }
        //public Guid DocumentId { get; set; }
        // public DocumentSignerType DocumentSignerType { get; set; }
        public string DocumentName { get; set; }
        public DocumentStatus documentStatus { get; set; }
    }
   
}
