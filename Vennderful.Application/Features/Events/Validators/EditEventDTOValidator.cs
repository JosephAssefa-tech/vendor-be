using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Validators
{
    public class EditEventDTOValidator : AbstractValidator<EditEventRequestDTO>
    {
        public EditEventDTOValidator()
        {
            RuleFor(p => p.EventName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} can not exceed more than 50 characters");
        }
    }
}
