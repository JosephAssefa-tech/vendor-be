using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class TypeOfBusiness: BaseAuditableEntity
    {
        public string BusinessName { get; set; }
    }
}
