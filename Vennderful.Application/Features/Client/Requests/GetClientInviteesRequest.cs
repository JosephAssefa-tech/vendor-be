using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.Client.Responses;

namespace Vennderful.Application.Features.Client.Requests
{
    public class GetClientInvitesRequest : IRequest<GetClientInvitesResponse>
    {
        public string CompanyId { get; set; }
    }
}
