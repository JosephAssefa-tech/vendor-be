using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
    public class EventFinance : BaseAuditableEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid PackageId { get; set; }
        public Package Package { get; set; }
        public decimal PackagePrice { get; set; }
        public List<Addon> Addons { get; set; }
        public decimal DepositAmount { get; set; }
        public DateTime DepositDueDate { get; set; }
        public decimal TravelFees { get; set; }
        public PaymentStatus DepositStatus { get; set; }
       
    }
    [NotMapped]
    public class Addon
    {
        public Guid Id { get; set; } 
        public string AddOnName { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }

}
