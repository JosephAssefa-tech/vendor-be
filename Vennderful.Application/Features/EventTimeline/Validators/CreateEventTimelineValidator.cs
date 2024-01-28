using FluentValidation;
using Vennderful.Application.Features.EventTimeline.DTOs;

namespace Vennderful.Application.Features.EventTimeline.Validators
{
    public class CreateEventTimelineValidator : AbstractValidator<CreateEventTimelineDto>
    {
        public CreateEventTimelineValidator()
        {
            RuleFor(p => p.SlotTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
