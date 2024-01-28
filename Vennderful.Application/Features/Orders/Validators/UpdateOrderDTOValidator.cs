using FluentValidation;
using System;
using Vennderful.Application.Features.Orders.DTOs;

namespace Vennderful.Application.Features.Orders.Validators
{
    public class UpdateOrderDTOValidator : AbstractValidator<UpdateOrderDTO>
    {
        public UpdateOrderDTOValidator()
        {
            RuleFor(p => p.OrderDate)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("{PropertyName} should be greate than today");
        }
    }
}
