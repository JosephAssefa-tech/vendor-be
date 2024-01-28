using System;
using System.Collections.Generic;
using System.Text;

namespace Vennderful.Application.Models.UploadDocuments
{
    public class UploadedDocumentsUrlsDto
    {
        public ICollection<string> Urls { get; }
        public UploadedDocumentsUrlsDto(ICollection<string> urls)
        {
            Urls = urls;
        }
    }
}
