using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventDocuments.Dto;

namespace Vennderful.Application.Features.EventDocuments.Responses
{
    public class CreateEventDocumentResponse : BaseResponse
    {
        public CreateEventDocumentsDto Data { get; set; }
    }
}
