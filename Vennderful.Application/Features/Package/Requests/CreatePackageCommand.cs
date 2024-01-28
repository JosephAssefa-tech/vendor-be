using MediatR;
using Vennderful.Application.Features.Package.DTOs;
using Vennderful.Application.Features.Package.Responses;

namespace Vennderful.Application.Features.Package.Requests
{
    public class CreatePackageCommand : IRequest<CreatePackageResponse>
    {
        public CreatePackageDTO CreatePackageDTO { get; set; }
    }
}
