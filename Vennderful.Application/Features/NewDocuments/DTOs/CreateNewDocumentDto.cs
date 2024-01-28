using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.NewDocuments.DTOs
{
    public class CreateNewDocumentDto: BaseDTO
    {
        public string DocumentName { get; set; }
        public string? DocumentBody { get; set; }
        public string? DocumentDescription { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
        public Guid EventId { get; set; }
        public Guid CompanyId { get; set; }
        public string? DocumentUrl { get; set; }
    }
}
