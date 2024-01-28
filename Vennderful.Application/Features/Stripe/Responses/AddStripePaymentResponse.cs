using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Common;
using Vennderful.Application.Features.Stripe.DTOs;

namespace Vennderful.Application.Features.Stripe.Responses
{
    public class AddStripePaymentResponse : BaseResponse
    {
        public AddStripePaymentDTO Data { get; set; }
    }
}
