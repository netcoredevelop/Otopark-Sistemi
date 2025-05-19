using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class LinkingRegionConfiguration : IEntityTypeConfiguration<LinkingRegion>
{
    public void Configure(EntityTypeBuilder<LinkingRegion> builder)
    {
        builder.HasKey(lr => lr.Id);
        builder.HasMany(lr => lr.Vehicles)
               .WithOne(v => v.LinkingRegion)
               .HasForeignKey(v => v.LinkingRegionId);
    }
}