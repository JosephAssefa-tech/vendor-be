using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Orders.DTOs;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Customers.DTOs
{
    public class CustomerDTO : BaseDTO
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public decimal? CreditLimit { get; set; }
        public virtual IList<ListOrderDTO> Orders { get; set; }
    }
}
