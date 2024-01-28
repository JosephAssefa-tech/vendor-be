using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;

namespace Vennderful.Application.Features.Client.Responses
{
    public class CreateClientResponse : BaseResponse
    {
        public CreateClientDTO Data { get; set; }
    }
}
