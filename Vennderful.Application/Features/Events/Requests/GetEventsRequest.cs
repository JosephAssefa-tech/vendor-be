using MediatR;
using System;
using Vennderful.Application.Features.Events.Responses;

namespace Vennderful.Application.Features.Events.Requests
{
    public class GetEventsRequest : IRequest<GetEventsResponse>
    {
        public Guid CompanyId { get; set; }
    }
}
