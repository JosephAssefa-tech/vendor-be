using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.Stripe.DTOs
{
    public class AddStripePaymentDTO : BaseDTO
    {
        public string CustomerId { get; set; }
        public string ReceiptEmail { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
        public Guid CompanyId { get; set; }
    }
}
