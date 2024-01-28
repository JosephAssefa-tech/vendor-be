using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.User.DTOs
{
    public class CreateUserInvitationDTO
    {
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string Status { get; set; } = "Invited";
        public Guid CompanyId { get; set; }
        public string? EventId { get; set; }
        public string JobTitle { get; set; }
    }
}
