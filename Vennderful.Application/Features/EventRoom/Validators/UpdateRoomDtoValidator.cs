using FluentValidation;
using Vennderful.Application.Features.EventRoom.Dto;

namespace Vennderful.Application.Features.EventRoom.Validators
{
    public class UpdateRoomDtoValidator : AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomDtoValidator()
        {
            RuleFor(p => p.RoomName)
                .NotEmpty().WithMessage("{RoomName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{RoomName} can not exceed more than 200 characters");
        }

    }
}
