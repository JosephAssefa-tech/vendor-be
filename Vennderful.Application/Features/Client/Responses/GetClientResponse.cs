using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;

namespace Vennderful.Application.Features.Client.Responses
{
    public class GetClientResponse : BaseResponse
    {
        public ClientDTO Data { get; set; }
    }
}
