using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Entities;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.UserRoles.DTOs
{
    public class UserRoleDTO : BaseDTO
    {
        public Guid CompanyId { get; set; }
        public UserRoleType UserRoleType { get; set; }
        

    }
}
