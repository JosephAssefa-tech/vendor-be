using Vennderful.Application.Common;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Application.Features.Client.DTOs
{
    public class CreateClientDTO : BaseDTO
    {
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender Gender { get; set; }
        public AccountType AccountType { get; set; }
        public string? CompanyName { get; set; }
        public Phone[] Phone { get; set; }
        public Address Address { get; set; }
        public string Note { get; set; }
    }
}
