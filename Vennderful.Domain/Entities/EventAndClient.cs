using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Domain.Common;

namespace Vennderful.Domain.Entities
{
    public class EventAndClient : BaseAuditableEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid CompanyId { get; set; }
        public List<Guid> ClientId { get; set; }
        public List<Client> Clients { get; set; }
    }

}
