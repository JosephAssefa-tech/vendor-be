using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventAndMember.DTO;

namespace Vennderful.Application.Features.EventAndMember.Responses
{
    public class GetEventAndMembersResponse : BaseResponse
    {
        public List<ListEventAndMembersDTO> Data { get; set; }
    }
}
