using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.NewDocuments.DTOs;

namespace Vennderful.Application.Features.NewDocuments.Responses
{
    public class GetDocumentTemplateListResponse : BaseResponse
    {
        public List<NewDocumentDto> Data { get; set; }
    }
}
