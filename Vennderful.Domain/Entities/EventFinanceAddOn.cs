using System;

namespace Vennderful.Domain.Entities
{
    public class EventFinanceAddOn
    {
        public int Id { get; set; }
        public Guid EventFinanceId { get; set; }
        public EventFinance EventFinance { get; set; }
        public Guid AddOnId { get; set; }
        public AddOn AddOn { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
