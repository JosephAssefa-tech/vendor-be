using Vennderful.Persistence.Contexts;
using Vennderful.Persistence.Repositories;
using Vennderful.Identity.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vennderful.Identity.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Vennderful.Identity.Repositories;
using Vennderful.Identity.Model;

namespace Vennderful.Identity
{
      public static class IdentityServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService<ApplicationUser>, TokenService>();
            
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<VennderfulIdentityDBContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            }
            else
            {
                services.AddDbContext<VennderfulIdentityDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Vennderful_IdentityDB")));
              

            }

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<VennderfulIdentityDBContext>()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;

            });

            services.AddScoped(typeof(IAsyncIdentityRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IidentityUnitOfWork, IdentityUnitOfWork>();
      

            services.AddTransient<IUserRepository, UserRepository>((c) =>
                 new UserRepository(c.GetRequiredService<VennderfulIdentityDBContext>()));

            return services;
        }
    }
}

