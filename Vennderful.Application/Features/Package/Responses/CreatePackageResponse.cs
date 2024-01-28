using Vennderful.Application.Common;
using Vennderful.Application.Features.Package.DTOs;

namespace Vennderful.Application.Features.Package.Responses
{
    public class CreatePackageResponse : BaseResponse
    {
        public CreatePackageResponseDTO Data { get; set; }
    }
}
