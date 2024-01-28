using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.EventPayment.Responses;

namespace Vennderful.Application.Features.EventPayment.Requests
{
    public class GetEventPaymentByClientIdRequest : IRequest<GetEventPaymentByClientIdResponse>
    {
        public Guid EventId { get; set; }
        public Guid ClientId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
