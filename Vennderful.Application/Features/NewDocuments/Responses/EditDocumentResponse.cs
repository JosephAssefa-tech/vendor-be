using Vennderful.Application.Common;
using Vennderful.Application.Features.Documents.DTOs;

namespace Vennderful.Application.Features.Documents.Responses
{
    public class EditDocumentResponse : BaseResponse
    {
        public EditDocumentDto Data { get; set; }
    }
}
