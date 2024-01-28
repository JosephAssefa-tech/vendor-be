using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Package.DTOs;

namespace Vennderful.Application.Features.Package.Validators
{
    public class CreatePackageDTOValidator : AbstractValidator<CreatePackageDTO>
    {
        public CreatePackageDTOValidator()
        {
            RuleFor(p => p.PackageName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");
        }
    }
}
