using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Domain.Entities;

namespace Vennderful.Persistence.Configurations
{
    public class AddOnConfiguration : IEntityTypeConfiguration<AddOn>
    {
        public void Configure(EntityTypeBuilder<AddOn> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(c => c.AddOnName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.PricePerUnit)
                .IsRequired();
        }
    }
}
