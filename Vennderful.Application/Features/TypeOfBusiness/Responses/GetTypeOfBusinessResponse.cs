using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.TypeOfBusiness.DTOs;

namespace Vennderful.Application.Features.TypeOfBusiness.Responses
{
    public class GetTypeOfBusinessResponse: BaseResponse
    {
        public TypeOfBusinessDto Data { get; set; }
    }
}
