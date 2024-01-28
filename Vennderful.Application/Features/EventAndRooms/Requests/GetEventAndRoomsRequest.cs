using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventAndRooms.Responses;

namespace Vennderful.Application.Features.EventAndRooms.Requests
{
    public class GetEventAndRoomsRequest: IRequest<GetEventAndRoomsResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
