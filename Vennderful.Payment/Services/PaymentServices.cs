using Stripe;
using Vennderful.Application.Contracts.Payment;
using Vennderful.Application.Features.Stripe.DTOs;
using Vennderful.Domain.Entities;

namespace Vennderful.Payment.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;

        public PaymentServices(
            ChargeService chargeService,
            CustomerService customerService,
            TokenService tokenService)
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Create a new customer at Stripe through API using customer and card details from records.
        /// </summary>
        /// <param name="customer">Stripe Customer</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Stripe Customer</returns>
        public async Task<StripeCustomerDTO> AddStripeCustomerAsync(AddStripeCustomerDTO stripeCustomer, CancellationToken ct)
        {
            // Set Stripe Token options based on customer data
            TokenCreateOptions tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Name = stripeCustomer.Name,
                    Number = stripeCustomer.CreditCard.CardNumber,
                    ExpYear = stripeCustomer.CreditCard.ExpirationYear,
                    ExpMonth = stripeCustomer.CreditCard.ExpirationMonth,
                    Cvc = stripeCustomer.CreditCard.Cvc
                }
            };

            // Create new Stripe Token
            Token stripeToken = await _tokenService.CreateAsync(tokenOptions, null, ct);

            // Set Customer options using
            CustomerCreateOptions customerOptions = new CustomerCreateOptions
            {
                Name = stripeCustomer.Name,
                Email = stripeCustomer.Email,
                Source = stripeToken.Id
            };

            // Create customer at Stripe
            Stripe.Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);

            // Return the created customer at stripe
            return new StripeCustomerDTO(createdCustomer.Name, createdCustomer.Email, createdCustomer.Id);
        }

        /// <summary>
        /// Add a new payment at Stripe using Customer and Payment details.
        /// Customer has to exist at Stripe already.
        /// </summary>
        /// <param name="payment">Stripe Payment</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>Stripe Payment</returns>
        public async Task<StripePaymentDTO> AddStripePaymentAsync(AddStripePaymentDTO stripePayment, CancellationToken ct)
        {
            // Set the options for the payment we would like to create at Stripe
            ChargeCreateOptions paymentOptions = new ChargeCreateOptions
            {
                Customer = stripePayment.CustomerId,
                ReceiptEmail = stripePayment.ReceiptEmail,
                Description = stripePayment.Description,
                Currency = stripePayment.Currency,
                Amount = stripePayment.Amount
            };

            // Create the payment
            var createdPayment = await _chargeService.CreateAsync(paymentOptions, null, ct);

            // Return the payment to requesting method
            return new StripePaymentDTO(
              createdPayment.CustomerId,
              createdPayment.ReceiptEmail,
              createdPayment.Description,
              createdPayment.Currency,
              createdPayment.Amount,
              createdPayment.Id);
        }
    }
}