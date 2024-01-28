using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Member.Responses;

namespace Vennderful.Application.Features.Member.Requests
{
    public class GetMembersRequest : IRequest<GetMembersResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
