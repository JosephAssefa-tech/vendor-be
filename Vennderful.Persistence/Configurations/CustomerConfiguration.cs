
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vennderful.Domain.Entities;

namespace Vennderful.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder) 
        {
            builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.OwnsOne(c => c.Address);

        }
    }
}
