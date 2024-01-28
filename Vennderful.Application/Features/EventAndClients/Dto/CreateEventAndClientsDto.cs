using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.EventAndClients.Dto
{
    public class CreateEventAndClientsDto : BaseDTO
    {
        public Guid CompanyId { get; set; }
        public Guid EventId { get; set; }
        public List<Guid> ClientId { get; set; }
    }
}
