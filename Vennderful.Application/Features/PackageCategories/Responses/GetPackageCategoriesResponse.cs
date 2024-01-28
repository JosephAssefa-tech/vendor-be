using System;
using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.PackageCategories.DTOs;

namespace Vennderful.Application.Features.PackageCategories.Responses
{
    public class GetPackageCategoriesResponse : BaseResponse
    {
        public List<PackageCategoryDTo> Data { get; set; }
    }
}
