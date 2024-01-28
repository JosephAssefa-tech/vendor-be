using System.Collections.Generic;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventPayment.DTOs;

namespace Vennderful.Application.Features.EventPayment.Responses
{
    public class GetEventPaymentsResponse : BaseResponse
    {
        public List<EventPaymentDTO> Data { get; set; }
    }
}
