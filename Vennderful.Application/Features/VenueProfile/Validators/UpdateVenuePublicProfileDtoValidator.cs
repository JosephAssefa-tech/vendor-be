using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.VenueProfile.DTOs;

namespace Vennderful.Application.Features.VenueProfile.Validators
{
    public class UpdateVenuePublicProfileDtoValidator : AbstractValidator<UpdateVenuePublicProfileDTO>
    {
        public UpdateVenuePublicProfileDtoValidator()
        {
            RuleFor(p => p.VenueAccountInformationId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
