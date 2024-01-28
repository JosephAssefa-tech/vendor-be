using MediatR;
using System;
using Vennderful.Application.Features.Package.Responses;

namespace Vennderful.Application.Features.Package.Requests
{
    public class GetPackagesRequest : IRequest<GetPackagesResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
