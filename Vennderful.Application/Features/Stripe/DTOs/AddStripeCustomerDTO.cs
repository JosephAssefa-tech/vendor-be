using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection.Emit;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Stripe.DTOs
{
    public class AddStripeCustomerDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public decimal? CreditLimit { get; set; }
        public AddStripeCardDTO CreditCard { get; set; }

        public AddStripeCustomerDTO() { }

        public AddStripeCustomerDTO(string _name, string _email, Address _address, CustomerType _customerType, decimal _creditLimit, AddStripeCardDTO _creditcard)
        {
            Name = _name;
            Email = _email;
            Address = _address;
            CustomerType = _customerType;
            CreditLimit = _creditLimit;
            CreditCard = _creditcard;
        }
    }
}
