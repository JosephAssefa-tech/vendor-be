using System;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.EventDocumentSigners.DTOs
{
    public class EventDocumentSignerDTO
    {
        public string SentFrom { get; set; }
        public DateTime? LastChange { get; set; }
        public DateTime SentDate { get; set; }
        public string SignerName { get; set; }
    }
}
