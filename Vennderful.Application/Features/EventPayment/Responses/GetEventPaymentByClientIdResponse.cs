using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.EventPayment.DTOs;

namespace Vennderful.Application.Features.EventPayment.Responses
{
    public class GetEventPaymentByClientIdResponse : BaseResponse
    {
        public EventPaymentAmountDTO Data { get; set; }
    }
}
