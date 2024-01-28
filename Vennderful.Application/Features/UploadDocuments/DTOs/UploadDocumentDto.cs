using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.UploadDocuments.DTOs
{
    public class UploadDocumentDto
    {
        //public string DocumentName { get; set; }
        //public string DocumentUrl { get; set; }
        public IFormFile DocumentFile { get; set; }
        public DocumentCategory Category { get; set; }
        public Guid CompanyId { get; set; }
    }
}
