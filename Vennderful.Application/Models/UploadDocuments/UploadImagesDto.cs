using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Vennderful.Application.Extensions;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Models.UploadDocuments
{
    public class UploadImagesDto
    {
        public Stream Content { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Category { get; set; }  // for now this can be package, document or addon or other
        public string CompanyId { get; set; }
    }
}
