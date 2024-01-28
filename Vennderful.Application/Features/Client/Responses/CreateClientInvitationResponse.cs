using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;

namespace Vennderful.Application.Features.Client.Responses
{
    public class CreateClientInvitationResponse : BaseResponse
    {
        public List<ClientInvitationDTO> Data { get; set; }
    }
}
