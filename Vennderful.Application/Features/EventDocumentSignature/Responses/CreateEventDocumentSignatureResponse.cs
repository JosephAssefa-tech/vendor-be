using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventDocumentSignature.Dto;

namespace Vennderful.Application.Features.EventDocumentSignature.Responses
{
    public class CreateEventDocumentSignatureResponse : BaseResponse
    {
        CreateEventDocumentSignatureDTO Data { get; set; }
    }
}
