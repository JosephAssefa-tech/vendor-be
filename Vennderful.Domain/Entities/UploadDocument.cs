using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class UploadDocument: BaseAuditableEntity
    {
        public string DocumentName { get; set; }
        public string DocumentUrl { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
    }
}
