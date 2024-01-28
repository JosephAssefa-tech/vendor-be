using MediatR;
using System;
using Vennderful.Application.Features.NewDocuments.Responses;
namespace Vennderful.Application.Features.NewDocuments.Requests
{
  
    public class GetDocumentTemplateListQuery : IRequest<GetDocumentTemplateListResponse>
    {
        public string TemplayeName { get; set; }
        public Guid CompanyId { get; set; }
    }
}
