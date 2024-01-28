using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Package.DTOs;

namespace Vennderful.Application.Features.Package.Responses
{
    public class GetPackagesResponse : BaseResponse
    {
        public List<ListPackageDTO> Data { get; set; }
    }
}
