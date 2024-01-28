using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.TypeOfBusiness.DTOs;

namespace Vennderful.Application.Features.TypeOfBusiness.Responses
{
    public class GetTypesOfBusinessResponse: BaseResponse
    {
        public List<ListTypeOfBusinessDto> Data { get; set; }
    }
}
