using FluentValidation;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.Events.Validators
{
    public class CreateEventDTOValidator : AbstractValidator<CreateEventDTO>
    {
        public CreateEventDTOValidator()
        {
            RuleFor(p => p.EventName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");
        }
    }
}
