using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.UploadDocuments.DTOs;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.UploadDocuments.Responses
{
    public class DocumentResponse: BaseResponse
    {
    public string DocumentUrl { get; set; }
    public DocumentCategory Category { get; set; }
    public string DocumentName { get; set; }
    public Guid CompanyId { get; set; }

    }
}
