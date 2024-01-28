using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class Document: BaseAuditableEntity
    {
        public string DocumentName { get; set; }
        public string? DocumentBody { get; set; }
        public string? DocumentDescription { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
        public Guid CompanyId { get; set; }
        public string? DocumentUrl { get; set; }

    }
}
