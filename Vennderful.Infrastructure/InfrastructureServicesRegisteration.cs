using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vennderful.Application.Contracts.Infrastructure.Mail;
using Vennderful.Application.Contracts.Interfaces;
using Vennderful.Application.Models.Mail;
using Vennderful.Infrastructure.File;
using Vennderful.Infrastructure.Mail;

namespace Vennderful.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IFileService, FileService>();

            return services;
        }
    }

}
