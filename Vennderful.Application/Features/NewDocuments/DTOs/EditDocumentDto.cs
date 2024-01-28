using System;
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.Documents.DTOs
{
    public class EditDocumentDto : BaseDTO
    {
        public string DocumentName { get; set; }
        public string? DocumentDescription { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
        public string DocumentBody { get; set; }
        public Guid CompanyId { get; set; }
    }
}
