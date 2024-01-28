using System;
using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.AddOnsCategories.DTOs;

namespace Vennderful.Application.Features.AddOnsCategories.Responses
{
    public class GetAddOnsCategoryResponse : BaseResponse
    {
        public List<AddOnCategoryDTo> Data { get; set; }
    }
}
