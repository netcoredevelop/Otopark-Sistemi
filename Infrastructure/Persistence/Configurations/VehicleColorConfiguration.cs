using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class VehicleColorConfiguration : IEntityTypeConfiguration<VehicleColor>
{
    public void Configure(EntityTypeBuilder<VehicleColor> builder)
    {
        builder.HasKey(vc => vc.Id);
        builder.HasMany(vc => vc.Vehicles)
               .WithOne(v => v.VehicleColor)
               .HasForeignKey(v => v.VehicleColorId);

    }
}
