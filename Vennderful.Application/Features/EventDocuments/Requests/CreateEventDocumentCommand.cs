using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventDocuments.Dto;
using Vennderful.Application.Features.EventDocuments.Responses;

namespace Vennderful.Application.Features.EventDocuments.Requests
{
    public class CreateEventDocumentCommand: IRequest<CreateEventDocumentResponse>
    {
       public CreateEventDocumentsDto CreateEventDocument { get; set; }

    }
}
