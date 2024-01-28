using System;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Customers.DTOs;

namespace Vennderful.Application.Features.Orders.DTOs
{
    public class ListOrderDTO : BaseDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public virtual ListCustomerDTO Customer { get; set; }

    }
}
