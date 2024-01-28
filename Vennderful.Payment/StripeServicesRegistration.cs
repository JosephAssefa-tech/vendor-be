using System;
using Stripe;
using Vennderful.Payment.Services;
using Vennderful.Application.Contracts.Payment;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vennderful.Payment
{
    public static class StripeServicesRegistration
    {
        public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:SecretKey");

            return services
                .AddScoped<CustomerService>()
                .AddScoped<ChargeService>()
                .AddScoped<TokenService>()
                .AddScoped<IPaymentServices, PaymentServices>();
        }
    }
}

