using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Orders.DTOs;
using Vennderful.Domain.Entities;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Stripe.DTOs
{
    public class StripeCustomerDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address? Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public decimal? CreditLimit { get; set; }
        public string CustomerId { get; set; }

        public StripeCustomerDTO(string _name, string _email, string _id)
        {
            this.Name = _name;
            this.Email = _email;
            this.CustomerId = _id;
        }
    }
}
