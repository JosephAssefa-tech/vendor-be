using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.NewDocuments.DTOs;

namespace Vennderful.Application.Features.NewDocuments.Validators
{
    public class CreateEventDocumentSignatureDtoValidator: AbstractValidator<CreateEventDocumentSignatureNotificationDto>
    {
        public CreateEventDocumentSignatureDtoValidator()
        {
            RuleFor(p => p.DocumentId)
           .NotNull().WithMessage("{DocumentId} is required.");
          
            RuleFor(p => p.EventId)
          .NotNull().WithMessage("{EventId} is required.");

            RuleFor(p => p.ClientId)
           .NotNull().WithMessage("{ClientId} is required.");
        }
    }
}
