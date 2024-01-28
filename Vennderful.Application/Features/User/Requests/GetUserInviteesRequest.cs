using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.User.Responses;

namespace Vennderful.Application.Features.User.Requests
{
    public class GetUserInvitesRequest : IRequest<GetUserInvitesResponse>
    {
        public string CompanyId { get; set; }
    }
}
