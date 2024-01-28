using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class SocialProfile: BaseAuditableEntity
    {
        public string? SocialProfileName { get; set; }
        public string? SocialProfileLink { get; set; }
        public Guid CompanyId { get; set; }
    }
}
