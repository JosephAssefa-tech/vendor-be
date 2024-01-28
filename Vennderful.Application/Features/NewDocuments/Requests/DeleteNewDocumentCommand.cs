using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.NewDocuments.Responses;

namespace Vennderful.Application.Features.NewDocuments.Requests
{
    public class DeleteNewDocumentCommand: IRequest<DeleteNewDocumentResponse>
    {
        public Guid Id { get; set; }
        public Guid companyId { get; set; }

    }
}
