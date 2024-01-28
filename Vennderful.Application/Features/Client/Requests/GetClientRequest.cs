using MediatR;
using System;
using Vennderful.Application.Features.Client.Responses;

namespace Vennderful.Application.Features.Client.Requests
{
    public class GetClientRequest : IRequest<GetClientResponse>
    {
        public string CompanyId { get; set; }
        public string ClientId { get; set; }
    }
}
