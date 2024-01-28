using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventDocuments.Dto;

namespace Vennderful.Application.Features.EventDocuments.Responses
{
    public class GetEventDocumentResponse: BaseResponse
    {
        public List<ListEventDocumentDto> Data { get; set; }
    }
}
