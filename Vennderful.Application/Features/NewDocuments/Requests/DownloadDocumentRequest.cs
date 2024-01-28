using MediatR;
using System;
using Vennderful.Application.Features.NewDocuments.Responses;

namespace Vennderful.Application.Features.NewDocuments.Requests
{
    public class DownloadDocumentRequest : IRequest<DownloadDocumentResponse>
    {
        public Guid Id { get; set; }
    }
}
