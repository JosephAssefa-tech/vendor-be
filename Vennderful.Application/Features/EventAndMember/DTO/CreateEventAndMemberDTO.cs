using System;
using System.Collections.Generic;
using System.Text;

namespace Vennderful.Application.Features.EventAndMember.DTO
{
    public class CreateEventAndMemberDTO
    {
        public Guid EventId { get; set; }
        public Guid MemberId { get; set; }
        public bool IsActive { get; set; }
    }
}
