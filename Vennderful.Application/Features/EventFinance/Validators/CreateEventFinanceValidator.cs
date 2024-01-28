using FluentValidation;
using Vennderful.Application.Features.EventFinance.Dto;

namespace Vennderful.Application.Features.EventFinance.Validators
{
    public class CreateEventFinanceValidator : AbstractValidator<CreateEventFinanceDto>
    {
        public CreateEventFinanceValidator()
        {
            RuleFor(p => p.EventId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
