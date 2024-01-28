using Vennderful.Persistence.Contexts;
using Vennderful.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vennderful.Application.Contracts.Persitence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace Vennderful.Persistence
{
    public static class PersistenceServicesRegisteration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
                    
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<VennderfulDbContext>(options =>
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            }
            else
            {
                services.AddDbContext<VennderfulDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("VennderfulDB")));
                
            }               
           
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IAddOnCategoryRepository, AddOnCategoryRepository>();
            services.AddScoped<IRateStructureRepository, RateStructureRepository>();
            services.AddScoped<ITypeOfBusinessRepository, TypeOfBusinessRepository>();
            services.AddScoped<IVenueAccountInformationRepository, VenueAccountInformationRepository>();
            services.AddScoped<IAddOnRepository, AddOnRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IPackageCategoryRepository, PackageCategoryRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IVenuePublicProfileRepository, VenuePublicProfileRepository>();
            services.AddScoped<IWorkingHoursRepository, WorkingHoursRepository>();
            services.AddScoped<ISocialProfileRepository, SocialProfileRepository>();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>((c) =>
                 new UserProfileRepository(c.GetRequiredService<VennderfulDbContext>())
            );
            services.AddScoped<IRoomRepository, RoomRepository>();

            return services;
        }
    }
}
