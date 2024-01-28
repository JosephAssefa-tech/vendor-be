
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vennderful.Domain.Entities;

namespace Vennderful.Persistence.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder) 
        {
            builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(c => c.FirstName)
                .HasMaxLength(50);
            builder.OwnsOne(c => c.Address);
            builder.Navigation(p => p.Address).IsRequired();

        }
    }
}
