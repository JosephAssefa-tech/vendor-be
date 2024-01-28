using MediatR;
using Vennderful.Application.Features.Stripe.DTOs;
using Vennderful.Application.Features.Stripe.Responses;

namespace Vennderful.Application.Features.Stripe.Requests
{
    public class AddStripeCustomerCommand : IRequest<AddStripeCustomerResponse>
    {
        public AddStripeCustomerDTO AddStripeCustomerDTO { get; set; }
    }
}
