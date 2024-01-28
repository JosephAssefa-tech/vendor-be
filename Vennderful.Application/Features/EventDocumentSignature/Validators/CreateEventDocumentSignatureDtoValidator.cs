using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventDocumentSignature.Dto;

namespace Vennderful.Application.Features.EventDocumentSignature.Validators
{
    public class CreateEventDocumentSignatureDtoValidator : AbstractValidator<CreateEventDocumentSignatureDTO>
    {
        public CreateEventDocumentSignatureDtoValidator()
        {
            RuleFor(p => p.EventDocumentId)
                .NotEmpty().WithMessage("{EventDocumentId} is required.")
                .NotNull();
            RuleFor(p => p.SignerId)
            .NotEmpty().WithMessage("{SignerId} is required.")
            .NotNull();

        }
    }
}
