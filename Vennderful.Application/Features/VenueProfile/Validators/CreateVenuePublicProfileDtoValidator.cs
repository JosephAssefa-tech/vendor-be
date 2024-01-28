using FluentValidation;
using Vennderful.Application.Features.VenueProfile.DTOs;

namespace Vennderful.Application.Features.VenueProfile.Validators
{
    public class CreateVenuePublicProfileDtoValidator : AbstractValidator<CurateVenuePublicProfileDto>
    {
        public CreateVenuePublicProfileDtoValidator()
        {
            RuleFor(p => p.VenueAccountInformationId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
