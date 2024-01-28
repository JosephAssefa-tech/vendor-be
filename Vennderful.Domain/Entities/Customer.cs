
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public decimal? CreditLimit { get; set; }
        public virtual IList<Order> Orders { get; set; }
    }
}
