using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Domain.Enums;

namespace Vennderful.Application.Features.EventPayment.DTOs
{
    public class EventPaymentDTO : BaseDTO
    {
        public Guid EventId { get; set; }
        public Guid ClientId { get; set; }
        public string PaymentReason { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentNote { get; set; }
        public ClientDTO Client { get; set; }
    }
}
