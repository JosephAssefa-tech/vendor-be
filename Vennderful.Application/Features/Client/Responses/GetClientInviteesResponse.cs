using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Client.DTOs;

namespace Vennderful.Application.Features.Client.Responses
{
    public class GetClientInvitesResponse : BaseResponse
    {
        public List<GetClientInvitesDTO> Data { get; set; }
    }
}
