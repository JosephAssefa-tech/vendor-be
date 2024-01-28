using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Member.DTO;

namespace Vennderful.Application.Features.Member.Responses
{
    public class CreateMemberResponse : BaseResponse
    {
        public CreateMemberDTO Data { get; set; }
    }
}
