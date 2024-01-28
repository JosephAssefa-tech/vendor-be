using FluentValidation;
using Vennderful.Application.Features.Stripe.DTOs;

namespace Vennderful.Application.Features.Stripe.Validators
{
    public class AddStripeCustomerDTOValidator : AbstractValidator<AddStripeCustomerDTO>
    {
        public AddStripeCustomerDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");
        }
    }
}
