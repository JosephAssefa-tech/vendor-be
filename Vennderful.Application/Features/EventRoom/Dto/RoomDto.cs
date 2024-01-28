using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.EventRoom.Dto
{
    public class RoomDto: BaseDTO
    {
        public string RoomName { get; set; }
        public Guid CompanyId { get; set; }
        //public RoomStatus? RoomStatus { get; set; }
        //public virtual IList<EventDTO> Events { get; set; }
    }
}
