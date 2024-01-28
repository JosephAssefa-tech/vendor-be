
using Vennderful.Application.Common;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.Events.Responses
{
    public class GetEventResponse : BaseResponse
    {
        public ListEventDTO Data { get; set; }
    }
}
