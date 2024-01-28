using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.UploadDocuments.DTOs;
using Vennderful.Application.Features.UploadDocuments.Responses;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.UploadDocuments.Requests
{
    public class DocumentRequest:IRequest<DocumentResponse>
    {
        public IFormFile DocumentFile { get; set; }
        public DocumentCategory Category { get; set; }
        public Guid CompanyId { get; set; }

    }
}
