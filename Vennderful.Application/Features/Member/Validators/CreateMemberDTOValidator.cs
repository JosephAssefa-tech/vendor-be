using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Member.DTO;

namespace Vennderful.Application.Features.Member.Validators
{
    public class CreateMemberDTOValidator : AbstractValidator<CreateMemberDTO>
    {
        public CreateMemberDTOValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} should be valid email address.");
        }
    }
}
