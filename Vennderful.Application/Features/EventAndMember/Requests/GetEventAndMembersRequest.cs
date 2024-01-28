using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventAndMember.Responses;

namespace Vennderful.Application.Features.EventAndMember.Requests
{
    public class GetEventAndMembersRequest : IRequest<GetEventAndMembersResponse>
    {
        public Guid EventId { get; set; }
    }
}
