using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.AddOnsCategories.DTOs;

namespace Vennderful.Application.Features.AddOnsCategories.Responses
{
    public class GetAddOnCategoryResponse : BaseResponse
    {
        public AddOnCategoryDTo Data { get; set; }
    }
}
