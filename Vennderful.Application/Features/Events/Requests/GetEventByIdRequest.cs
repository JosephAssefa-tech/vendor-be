using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Requests
{
    public  class GetEventByIdRequest : IRequest<GetEventByIdResponse>
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
    }
}
