using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventAndRooms.Dto;

namespace Vennderful.Application.Features.EventAndRooms.Validators
{
    public class CreateEventAndRoomDtoValidator : AbstractValidator<CreateEventAndRoomDto>
    {
        public CreateEventAndRoomDtoValidator()
        {
            RuleFor(p => p.RoomId)
                .NotNull().WithMessage("RoomId is required.");
        }
    }
}
