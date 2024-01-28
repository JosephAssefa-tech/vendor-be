using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.NewDocuments.DTOs;

namespace Vennderful.Application.Features.NewDocuments.Responses
{
     public class GetDocumentTemplateNameListResponse : BaseResponse
    {
        public List<string> Data { get; set; }
    }
}
