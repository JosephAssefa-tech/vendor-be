using Vennderful.Application.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.NewDocuments.DTOs
{
    public class NewDocumentDto: BaseDTO
    {
        public string DocumentName { get; set; }
        public string? DocumentBody { get; set; }
        public string? DocumentDescription { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
        public string? DocumentUrl { get; set; }
    }

}
