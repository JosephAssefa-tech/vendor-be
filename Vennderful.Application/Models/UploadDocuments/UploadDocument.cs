using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Models.UploadDocuments
{
    public class UploadDocument
    {
        public string DocumentName { get; set; }
        public Stream Documents { get; set; }
        public string DocumentFormatType { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
    }
}
