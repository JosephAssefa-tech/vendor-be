using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.User.DTOs
{
    public class UserProfileDTO : BaseDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid CompanyId { get; set; }
        public string UserRole { get; set; }
    }
}
