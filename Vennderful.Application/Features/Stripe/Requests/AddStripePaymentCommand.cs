using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Vennderful.Application.Features.Stripe.DTOs;
using Vennderful.Application.Features.Stripe.Responses;

namespace Vennderful.Application.Features.Stripe.Requests
{
    public class AddStripePaymentCommand : IRequest<AddStripePaymentResponse>
    {
        public AddStripePaymentDTO AddStripePaymentDTO { get; set; }
    }
}
