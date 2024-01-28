using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Features.VenueAccount.DTOs;

namespace Vennderful.Application.Features.VenueAccount.Validators
{
    public class CompleteVenueCreationDTOValidator : AbstractValidator<CompleteVenueCreationDTO>
    {
        public CompleteVenueCreationDTOValidator()
        {
            RuleFor(p => p.CompanyName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.Status)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .NotEqual(Domain.Enums.CompanyProfileStatus.Pending).WithMessage("{PropertyName} should have another value.");
        }
    }
}
