using MediatR;
using System;
using Vennderful.Application.Features.AddOn.Responses;

namespace Vennderful.Application.Features.AddOn.Requests
{
    public class GetAddOnsRequest : IRequest<GetAddOnsResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
