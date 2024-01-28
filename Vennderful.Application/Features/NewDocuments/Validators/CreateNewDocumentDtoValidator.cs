using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.NewDocuments.DTOs;

namespace Vennderful.Application.Features.NewDocuments.Validators
{
    public class CreateNewDocumentDtoValidator:AbstractValidator<CreateNewDocumentDto>
    {
        public CreateNewDocumentDtoValidator()
        {
            RuleFor(p => p.DocumentName)
    .NotEmpty().WithMessage("{PropertyName} is required.")
    .NotNull()
    .MinimumLength(1).WithMessage("{PropertyName} can not less  than 1 character");

        }
    }
}
