using Vennderful.Application.Common;
using Vennderful.Application.Features.AddOn.DTOs;

namespace Vennderful.Application.Features.AddOn.Responses
{
    public class CreateAddOnResponse : BaseResponse
    {
        public CreateAddOnResponseDTO Data { get; set; }
    }
}
