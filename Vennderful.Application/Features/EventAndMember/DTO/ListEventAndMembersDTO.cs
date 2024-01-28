using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.EventAndMember.DTO
{
    public class ListEventAndMembersDTO : BaseDTO
    {
        public Guid EventId { get; set; }
        public Guid MemberId { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string JobTitle { get; set; }
        public string? ProfilePicture { get; set; }
        public string UserRole { get; set; }
    }
}
