using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.Stripe.DTOs
{
    public class StripePaymentDTO : BaseDTO
    {
        public string CustomerId { get; set; }
        public string ReceiptEmail { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
        public string PaymentId { get; set; }

        public StripePaymentDTO(
            string _customerId, 
            string _receiptEmail, 
            string _description, 
            string _currency, 
            long _amount, 
            string _paymentId)
        {
            this.CustomerId = _customerId;
            this.ReceiptEmail = _receiptEmail;
            this.Description = _description;
            this.Currency = _currency;                
            this.Amount = _amount;
            this.PaymentId = _paymentId;
        }

    }
}
