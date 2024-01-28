using System;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.Orders.DTOs
{
    public class UpdateOrderDTO : BaseDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
    }
}
