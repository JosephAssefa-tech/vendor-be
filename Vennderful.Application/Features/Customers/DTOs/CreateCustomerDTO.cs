
using Vennderful.Application.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Customers.DTOs
{
    public class CreateCustomerDTO : BaseDTO
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public CustomerType CustomerType { get; set; }
        public decimal? CreditLimit { get; set; }
    }
}
