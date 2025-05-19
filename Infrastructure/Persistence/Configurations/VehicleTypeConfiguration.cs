using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;
public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.HasKey(vt => vt.Id);
        builder.HasMany(vt => vt.Vehicles)
               .WithOne(v => v.VehicleType)
               .HasForeignKey(v => v.VehicleTypeId);

    }
}
