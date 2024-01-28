using System.Collections.Generic;
using Vennderful.Domain.Common;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;

namespace Vennderful.Domain.Entities
{
    public class Client : BaseAuditableEntity
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
        public IList<EventClient> EventClients { get; set; }
        //public virtual Event Event { get; set; }
        // public IList<EventAndClient> EventClients { get; set; } = new List<EventAndClient>();


    }
}
