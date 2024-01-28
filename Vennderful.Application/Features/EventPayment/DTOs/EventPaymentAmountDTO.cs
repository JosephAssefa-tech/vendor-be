using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.EventPayment.DTOs
{
    public class EventPaymentAmountDTO : BaseDTO
    {
        public decimal PaymentAmount { get; set; }
    }
}
