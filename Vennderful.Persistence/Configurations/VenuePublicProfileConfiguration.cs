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
    public class VenuePublicProfileConfiguration : IEntityTypeConfiguration<VenuePublicProfile>
    {
        public void Configure(EntityTypeBuilder<VenuePublicProfile> builder)
        {
            builder.Property(x => x.Id)
               .HasDefaultValueSql("gen_random_uuid()");
        }
    }
}
