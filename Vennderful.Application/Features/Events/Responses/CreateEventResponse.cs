using Vennderful.Application.Common;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.Events.Responses
{
    public class CreateEventResponse : BaseResponse
    {
        public CreateEventDTO Data { get; set; }
    }
}
