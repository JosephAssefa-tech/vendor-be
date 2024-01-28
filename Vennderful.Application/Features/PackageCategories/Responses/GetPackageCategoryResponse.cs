using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.PackageCategories.DTOs;

namespace Vennderful.Application.Features.PackageCategories.Responses
{
    public class GetPackageCategoryResponse : BaseResponse
    {
        public PackageCategoryDTo Data { get; set; }
    }
}
