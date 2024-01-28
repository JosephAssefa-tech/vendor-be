using System;
using Vennderful.Domain.Enums;

namespace Vennderful.Domain.Entities
{
   public class EventFinancePaymentSchedule
    {
        public int Id { get; set; }
        public Guid EventFinanceId { get; set; }
        public EventFinance EventFinance { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal ScheduleAmount { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
