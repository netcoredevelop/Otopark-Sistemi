using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;
public class VehicleBrandConfiguration : IEntityTypeConfiguration<VehicleBrand>
{
    public void Configure(EntityTypeBuilder<VehicleBrand> builder)
    {
        builder.HasKey(vb => vb.Id);
        builder.HasMany(vb => vb.Vehicles)
               .WithOne(v => v.VehicleBrand)
               .HasForeignKey(v => v.VehicleBrandId);
    }
}
