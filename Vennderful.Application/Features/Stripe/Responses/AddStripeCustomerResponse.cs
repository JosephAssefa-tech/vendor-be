using Vennderful.Application.Common;
using Vennderful.Application.Features.Stripe.DTOs;

namespace Vennderful.Application.Features.Stripe.Responses
{
    public class AddStripeCustomerResponse : BaseResponse
    {
        public AddStripeCustomerDTO Data { get; set; }
    }
}
