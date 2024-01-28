using MediatR;
using System;
using Vennderful.Application.Features.NewDocuments.Responses;

namespace Vennderful.Application.Features.NewDocuments.Requests
{
    public class GetNewDocumentsRequest : IRequest<GetNewDocumentResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
