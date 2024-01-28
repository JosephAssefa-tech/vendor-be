using System;
using System.Collections.Generic;
using System.Text;

namespace Vennderful.Application.Models.UploadDocuments
{
    public class UploadedImageUrlDTO
    {
        public string Url { get; set; }
        public UploadedImageUrlDTO(string url)
        {
            Url = url;
        }
    }
}
