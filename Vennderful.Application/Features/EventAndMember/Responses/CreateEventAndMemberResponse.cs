using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventAndMember.DTO;

namespace Vennderful.Application.Features.EventAndMember.Responses
{
    public class CreateEventAndMemberResponse : BaseResponse
    {
        public CreateEventAndMemberDTO Data { get; set; }
    }
}
