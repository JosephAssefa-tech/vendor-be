using FluentValidation;
using Vennderful.Application.Features.Documents.DTOs;

namespace Vennderful.Application.Features.Documents.Validators
{
    public class EditDocumentDtoValidator : AbstractValidator<EditDocumentDto>
    {
        public EditDocumentDtoValidator()
        {
            //RuleFor(p => p.DocumentBody)
            //            .NotEmpty().WithMessage("{PropertyName} is required.")
            //            .NotNull();
        }
    }
}
