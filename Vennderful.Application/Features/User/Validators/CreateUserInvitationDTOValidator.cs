using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.User.DTOs;

namespace Vennderful.Application.Features.User.Validators
{
    public class CreateUserInvitationDTOValidator : AbstractValidator<CreateUserInvitationDTO>
    {
        public CreateUserInvitationDTOValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} should be valid email address.");
            RuleFor(p => p.UserRole)
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
