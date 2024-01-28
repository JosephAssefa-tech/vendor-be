using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventDocuments.Responses;

namespace Vennderful.Application.Features.EventDocuments.Requests
{
    public class GetEventDocumentsRequest:IRequest<GetEventDocumentResponse>
    {
        public Guid EventId { get; set; }
    }
}
