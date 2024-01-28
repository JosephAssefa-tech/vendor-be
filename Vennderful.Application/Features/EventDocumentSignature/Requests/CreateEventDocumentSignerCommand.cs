using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventDocumentSignature.Dto;
using Vennderful.Application.Features.EventDocumentSignature.Responses;

namespace Vennderful.Application.Features.EventDocumentSignature.Requests
{
    public class CreateEventDocumentSignerCommand : IRequest<CreateEventDocumentSignatureResponse>
    {
        public CreateEventDocumentSignatureDTO CreateEventDocumentSignatureDto { get; set; }
    }
}
