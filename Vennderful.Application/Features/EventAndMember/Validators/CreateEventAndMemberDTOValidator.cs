using FluentValidation;
using Vennderful.Application.Features.EventAndMember.DTO;

namespace Vennderful.Application.Features.EventAndMember.Validators
{
    public class CreateEventAndMemberDTOValidator : AbstractValidator<CreateEventAndMemberDTO>
    {
        public CreateEventAndMemberDTOValidator()
        {
            RuleFor(p => p.EventId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(p => p.MemberId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
