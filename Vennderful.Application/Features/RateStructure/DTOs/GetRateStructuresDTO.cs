using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;

namespace Vennderful.Application.Features.RateStructure.DTOs
{
    public class GetRateStructuresDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
