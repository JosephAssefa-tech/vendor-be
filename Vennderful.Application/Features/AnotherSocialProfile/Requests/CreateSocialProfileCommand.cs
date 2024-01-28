using MediatR;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;
using Vennderful.Application.Features.AnotherSocialProfile.Responses;

namespace Vennderful.Application.Features.AnotherSocialProfile.Requests
{
    public class CreateSocialProfileCommand : IRequest<CreateSocialProfileResponse>
    {
        public CreateSocialProfileDto CreateSocialProfileDto { get;set;}

    }
}
