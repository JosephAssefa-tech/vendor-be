using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Requests
{
    public class GetEventClientsRequest : IRequest<GetEventClentsResponse>
    {
        public Guid Id { get; set; }
       
    }
}
