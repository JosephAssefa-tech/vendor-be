using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Vennderful.Identity;
using Vennderful.Identity.Model;

namespace Vennderful.Identity
{
     
        public class VennderfulIdentityDBContext : IdentityDbContext<ApplicationUser>
        {
            public VennderfulIdentityDBContext(DbContextOptions<VennderfulIdentityDBContext> options) : base(options)
            {
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                SeedRole(modelBuilder);
                base.OnModelCreating(modelBuilder);
            }

            private static void SeedRole(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "ead96b57-9833-449a-b61e-ba6b8c376f4c", Name = "Super Admin", NormalizedName = "Super Admin".ToUpper() });
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "653e479e-ac63-4146-8b37-d3764fcabd18", Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "d24b8665-3472-4497-b809-10a28336d314", Name = "Basic User", NormalizedName = "Basic User".ToUpper() });

        }
        }    
}
