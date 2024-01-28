using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Client.DTOs;

namespace Vennderful.Application.Features.Client.Responses
{
    public class GetClientsResponse : BaseResponse
    {
        public List<ClientDTO> Data { get; set; }
    }
}
