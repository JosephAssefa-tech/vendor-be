using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.EventRoom.Dto
{
    public class UpdateRoomDto: BaseDTO
    {
        public string RoomName { get; set; }
        public RoomStatus? RoomStatus { get; set; }

    }
}
