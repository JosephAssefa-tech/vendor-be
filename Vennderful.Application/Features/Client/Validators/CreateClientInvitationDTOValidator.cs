using FluentValidation;
using Vennderful.Application.Features.Client.DTOs;

namespace Vennderful.Application.Features.Client.Validators
{
    public class CreateClientInvitationDTOValidator : AbstractValidator<CreateClientDTO>
    {
        public CreateClientInvitationDTOValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} should be valid email address.");
        }
    }
}
