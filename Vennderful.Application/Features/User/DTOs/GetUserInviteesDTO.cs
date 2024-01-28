using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.User.DTOs
{
    public class GetUserInvitesDTO
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
