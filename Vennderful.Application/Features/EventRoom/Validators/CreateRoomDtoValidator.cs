using FluentValidation;
using Vennderful.Application.Features.EventRoom.Dto;

namespace Vennderful.Application.Features.EventRoom.Validators
{
    public class CreateRoomDtoValidator: AbstractValidator<CreateRoomDto>
    {
        public CreateRoomDtoValidator()
        {
            RuleFor(p => p.RoomName)
                .NotEmpty().WithMessage("{RoomName} is required.")
                .NotNull()
                .MinimumLength(2).WithMessage("{RoomName} minimum 2 characters allowed for room creation")
                .MaximumLength(50).WithMessage("{RoomName} can not exceed more than 50 characters");
        }
    }
}
