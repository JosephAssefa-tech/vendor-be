using FluentValidation;
using Vennderful.Application.Features.UserRoles.DTOs;

namespace Vennderful.Application.Features.UserRoles.Validators
{
    public class AddUserRoleDTOValidator : AbstractValidator<AddUserRoleDTO>
    {
       public AddUserRoleDTOValidator()
       {
            RuleFor(p => p.CompanyId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("{PropertyName} can not be null");
       }
}
}
