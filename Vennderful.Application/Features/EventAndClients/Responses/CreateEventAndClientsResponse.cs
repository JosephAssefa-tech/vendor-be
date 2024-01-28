using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventAndClients.Dto;

namespace Vennderful.Application.Features.EventAndClients.Responses
{
    public class CreateEventAndClientsResponse : BaseResponse
    {
        public CreateEventAndClientsDto Data { get; set; }
    }
}
