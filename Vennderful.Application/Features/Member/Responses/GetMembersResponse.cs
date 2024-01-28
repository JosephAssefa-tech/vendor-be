using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Member.DTO;

namespace Vennderful.Application.Features.Member.Responses
{
    public class GetMembersResponse : BaseResponse
    {
        public List<ListMembersDTO> Data { get; set; }
    }
}
