using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.AddOn.DTOs;

namespace Vennderful.Application.Features.AddOn.Responses
{
    public class GetAddOnsResponse : BaseResponse
    {
        public List<ListAddOnDTO> Data { get; set; }
    }
}
