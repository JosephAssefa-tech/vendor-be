using System.Threading.Tasks;
using System.Threading;
using Vennderful.Application.Features.Stripe.DTOs;

namespace Vennderful.Application.Contracts.Payment
{
    public interface IPaymentServices
    {
        Task<StripeCustomerDTO> AddStripeCustomerAsync(AddStripeCustomerDTO stripeCustomer, CancellationToken ct);
        Task<StripePaymentDTO> AddStripePaymentAsync(AddStripePaymentDTO stripePayment, CancellationToken ct);
    }
}
