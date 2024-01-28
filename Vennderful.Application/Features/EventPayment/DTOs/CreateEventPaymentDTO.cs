using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.EventPayment.DTOs
{
    public class CreateEventPaymentDTO
    {
        public Guid EventId { get; set; }
        public Guid ClientId { get; set; }
        public string PaymentReason { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string? PaymentNote { get; set; }
        public int? EventFinancePaymentScheduleId { get; set; }
    }
}
