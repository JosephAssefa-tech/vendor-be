using MediatR;
using System;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Responses;

namespace Vennderful.Application.Features.Client.Requests
{
    public class CreateClientCommand : IRequest<CreateClientResponse>
    {
        public CreateClientDTO CreateClientDTO { get; set; }
        public Guid EventId { get; set; }
    }
}
