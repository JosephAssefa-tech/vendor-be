using FluentValidation;
using Vennderful.Application.Features.WorkingHours.DTOs;

namespace Vennderful.Application.Features.WorkingHours.Validators
{
    public class CreateWorkingHourDtoValidator : AbstractValidator<CreateWorkingHourDto>
    {
        public CreateWorkingHourDtoValidator()
        {
        }
    }
}
