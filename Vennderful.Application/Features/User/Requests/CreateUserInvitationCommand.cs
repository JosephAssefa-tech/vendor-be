using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.User.DTOs;
using Vennderful.Application.Features.User.Responses;

namespace Vennderful.Application.Features.User.Requests
{
    public class CreateUserInvitationCommand : IRequest<CreateUserInvitationResponse>
    {
        public CreateUserInvitationDTO CreateUserInvitationDTO { get; set; }
    }
}
