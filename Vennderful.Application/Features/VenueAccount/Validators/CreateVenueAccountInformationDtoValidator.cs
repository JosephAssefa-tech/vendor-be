using FluentValidation;
using Vennderful.Application.Features.VenueAccount.DTOs;

namespace Vennderful.Application.Features.VenueAccount.Validators
{
    public class CreateVenueAccountInformationDtoValidator : AbstractValidator<CreateVenueAccountInformationDto>
    {
        public CreateVenueAccountInformationDtoValidator()
        {
            RuleFor(p => p.CompanyName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");


        }
    }
}
