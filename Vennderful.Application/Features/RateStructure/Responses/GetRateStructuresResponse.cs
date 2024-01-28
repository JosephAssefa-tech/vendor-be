using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.RateStructure.DTOs;

namespace Vennderful.Application.Features.RateStructure.Responses
{
    public class GetRateStructuresResponse : BaseResponse
    {
        public List<GetRateStructuresDTO> Data { get; set; }
    }
}
