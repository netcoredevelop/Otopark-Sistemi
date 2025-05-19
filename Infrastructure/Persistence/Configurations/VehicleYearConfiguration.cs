using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class VehicleYearConfiguration : IEntityTypeConfiguration<VehicleYear>
{
    public void Configure(EntityTypeBuilder<VehicleYear> builder)
    {
        builder.HasKey(vy => vy.Id);
        builder.HasMany(vy => vy.Vehicles)
               .WithOne(v => v.VehicleYear)
               .HasForeignKey(v => v.VehicleYearId);
    }
}
