using MediatR;
using Vennderful.Application.Features.Documents.DTOs;
using Vennderful.Application.Features.Documents.Responses;

namespace Vennderful.Application.Features.Documents.Requests
{
    public class EditDocumentCommand : IRequest<EditDocumentResponse>
    {
        public EditDocumentDto EditDocumentDto { get; set; }

    }
}
