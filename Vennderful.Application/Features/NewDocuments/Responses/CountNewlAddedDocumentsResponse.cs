using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.NewDocuments.DTOs;

namespace Vennderful.Application.Features.NewDocuments.Responses
{
    public class CountNewlAddedDocumentsResponse: BaseResponse
    {
        public CreateNewDocumentDto CreateNewDocumentDto { get; set; }
        public int CountDocuments { get; set; }
    }
}
