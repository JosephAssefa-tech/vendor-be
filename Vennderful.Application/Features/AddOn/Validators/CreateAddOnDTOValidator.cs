using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.AddOn.DTOs;

namespace Vennderful.Application.Features.AddOn.Validators
{
    public class CreateAddOnDTOValidator : AbstractValidator<CreateAddOnDTO>
    {
        public CreateAddOnDTOValidator()
        {
            RuleFor(p => p.AddOnName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");
        }
    }
}
