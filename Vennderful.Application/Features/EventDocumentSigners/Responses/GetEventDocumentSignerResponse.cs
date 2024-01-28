using Vennderful.Application.Common;
using Vennderful.Application.Features.EventDocumentSigners.DTOs;

namespace Vennderful.Application.Features.EventDocumentSigners.Responses
{
    public class GetEventDocumentSignerResponse : BaseResponse
    {
        public EventDocumentSignerDTO Data { get; set; }
    }
}
