using System;
using System.Collections.Generic;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class UserRole : BaseAuditableEntity
    {
        public Guid CompanyId { get; set; }
        public UserRoleType UserRoleType { get; set; }
        
    }
}
