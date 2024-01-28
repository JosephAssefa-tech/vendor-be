using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.EventDocuments.Dto
{
    public class ListEventDocumentDto: BaseDTO
    {
        public string DocumentName { get; set; }
        public DocumentStatus documentStatus { get; set; }
    }
}
