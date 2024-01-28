using FluentValidation;
using Vennderful.Application.Features.EventAndClients.Dto;

namespace Vennderful.Application.Features.EventAndClients.Validators
{
    public class CreateEventAndClientsDtoValidator : AbstractValidator<CreateEventAndClientsDto>

    {
        public CreateEventAndClientsDtoValidator()
        {
            RuleFor(p => p.ClientId)
                .NotNull().WithMessage("ClientIds is required.");
        }
    }
}
