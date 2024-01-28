using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventDocuments.Dto;

namespace Vennderful.Application.Features.EventDocuments.Validators
{
    public class CreateEventDocumentDtoValidator: AbstractValidator<CreateEventDocumentsDto>
    {
        public CreateEventDocumentDtoValidator()
        {
            RuleFor(p => p.EventId)
    .NotEmpty().WithMessage("{EventId} is required.")
    .NotNull();
            RuleFor(p => p.DocumentId)
  .NotEmpty().WithMessage("{DocumentId} is required.")
  .NotNull();

        }
    }
}
