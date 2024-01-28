using Vennderful.Application.Common;

namespace Vennderful.Application.Features.NewDocuments.Responses
{
    public class DownloadDocumentResponse : BaseResponse
    {
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentBody { get; set; } = string.Empty;
        public string? DocumentUrl { get; set; } = string.Empty;
        public string? MyProperty { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
}
