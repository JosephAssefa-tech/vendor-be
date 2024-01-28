using Vennderful.Application.Common;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;

namespace Vennderful.Application.Features.AnotherSocialProfile.Responses
{
    public class CreateSocialProfileResponse : BaseResponse
    {
        public CreateSocialProfileDto Data { get; set; }
    }
}
