using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Persistence.Configurations
{
    public class VenueAccountConfiguration : IEntityTypeConfiguration<VenueAccountInformation>
    {
        public void Configure(EntityTypeBuilder<VenueAccountInformation> builder)
        {
            builder.Property(x => x.Id)
               .HasDefaultValueSql("gen_random_uuid()");
            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Website)
                .IsRequired();
            builder.OwnsOne(c => c.Address);
        }
    }
}
