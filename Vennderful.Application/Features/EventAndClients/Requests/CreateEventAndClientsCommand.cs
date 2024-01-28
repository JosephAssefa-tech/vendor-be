using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventAndClients.Dto;
using Vennderful.Application.Features.EventAndClients.Responses;

namespace Vennderful.Application.Features.EventAndClients.Requests
{
    public class CreateEventAndClientsCommand: IRequest<CreateEventAndClientsResponse>
    {
        public CreateEventAndClientsDto CreateEventAndClientsDto { get; set; }

    }
}
