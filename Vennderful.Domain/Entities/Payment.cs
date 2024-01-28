using System;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class Payment : BaseAuditableEntity
    {
        public string CustomerId{ get; set; }
        public string ReceiptEmail { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
        public Guid CompanyId { get; set; }
    }
}
