using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.Events.Validators
{
    public class UpdateEventDTOValidator : AbstractValidator<UpdateEventDto>
    {
        public UpdateEventDTOValidator()
        {
            RuleFor(p => p.EventName)
                .NotEmpty().WithMessage("{EventName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{EventName} can not exceed more than 50 characters");
        }
    }
}
