using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Client.DTOs;

namespace Vennderful.Application.Features.Events.DTOs
{
    public class ListEventClientsDTO
    {
        public IList<ListClientDTO> Client { get; set; }
    }
}
