using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Stripe.DTOs
{
    public class AddStripeCardDTO : BaseDTO
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationYear { get; set; }
        public string ExpirationMonth { get; set; }
        public string Cvc { get; set; }

        public AddStripeCardDTO() { }

        public AddStripeCardDTO(string _name, string _cardNumber, string _expirationYear, string _expirationMonth, string _cvc)
        {
            Name = _name;
            CardNumber = _cardNumber;
            ExpirationYear = _expirationYear;
            ExpirationMonth = _expirationMonth;
            Cvc = _cvc;
        }
    }
}
