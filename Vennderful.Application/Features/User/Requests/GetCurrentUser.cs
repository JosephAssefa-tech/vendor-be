using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.User.Responses;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.Features.User.Requests
{
    public class GetCurrentUser : IRequest<GetCurrentUserResponse>
    {
        public Guid IdentityId { get; set; }
    }
}
