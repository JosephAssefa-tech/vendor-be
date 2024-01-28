using FluentValidation;
using Vennderful.Application.Features.EventPayment.DTOs;

namespace Vennderful.Application.Features.EventPayment.Validators
{
    public class CreateEventPaymentValidator : AbstractValidator<CreateEventPaymentDTO>
    {
        public CreateEventPaymentValidator()
        {
            RuleFor(p => p.EventId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(p => p.ClientId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
