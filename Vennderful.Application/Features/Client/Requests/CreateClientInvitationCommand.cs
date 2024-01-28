using MediatR;
using System;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Client.Responses;

namespace Vennderful.Application.Features.Client.Requests
{
    public class CreateClientInvitationCommand : IRequest<CreateClientInvitationResponse>
    {
        public CreateClientInvitationDTO CreateClientInvitationDTO { get; set; }
        public Guid EventId { get; set; }
    }
}
