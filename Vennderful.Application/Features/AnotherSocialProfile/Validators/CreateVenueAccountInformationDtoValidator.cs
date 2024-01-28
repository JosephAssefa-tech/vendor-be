using FluentValidation;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;

namespace Vennderful.Application.Features.AnotherSocialProfile.Validators
{
    public class CreateSocialProfileDtoValidator : AbstractValidator<CreateSocialProfileDto>
    {
        public CreateSocialProfileDtoValidator()
        {
            RuleFor(p => p.SocialProfileName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");


        }
    }
}
