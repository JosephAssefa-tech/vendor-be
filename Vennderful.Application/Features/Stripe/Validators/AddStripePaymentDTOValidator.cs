using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Stripe.DTOs;

namespace Vennderful.Application.Features.Stripe.Validators
{
    public class AddStripePaymentDTOValidator : AbstractValidator<AddStripePaymentDTO>
    {
        public AddStripePaymentDTOValidator()
        {
            RuleFor(p => p.CustomerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");
        }
    }
}
