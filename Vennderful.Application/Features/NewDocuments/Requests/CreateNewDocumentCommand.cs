using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.NewDocuments.Responses;

namespace Vennderful.Application.Features.NewDocuments.Requests
{
    public class CreateNewDocumentCommand: IRequest<CountNewlAddedDocumentsResponse>
    {
        public CreateNewDocumentDto CreateNewDocumentDto { get; set; }
    }
}
