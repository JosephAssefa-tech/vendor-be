using FluentValidation;
using Vennderful.Application.Features.EventTimeline.DTOs;

namespace Vennderful.Application.Features.EventTimeline.Validators
{
    public class UpdateEventTimelineValidator : AbstractValidator<UpdateEventTimelineDto>
    {
        public UpdateEventTimelineValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
