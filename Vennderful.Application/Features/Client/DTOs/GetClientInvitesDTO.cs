using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.Client.DTOs
{
    public class GetClientInvitesDTO
    {
        public string Email { get; set; }
        public string AccountType { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
