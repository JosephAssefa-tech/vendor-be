using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Events.DTOs;

namespace Vennderful.Application.Features.EventAndClients.Dto
{
    public class EventAndClientDto: BaseDTO
    {
        public Guid EventId { get; set; }
        public EventDTO Event { get; set; }
        public Guid ClientId { get; set; }
        public List<ClientDTO> Clients { get; set; }
    }
}
