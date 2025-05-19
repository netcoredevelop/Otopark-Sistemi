using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;
public class ParkLocationConfiguration : IEntityTypeConfiguration<ParkLocation>
{
    public void Configure(EntityTypeBuilder<ParkLocation> builder)
    {
        builder.HasKey(pl => pl.Id);
        builder.HasMany(pl => pl.Vehicles)
               .WithOne(v => v.ParkLocation)
               .HasForeignKey(v => v.ParkLocationId);
    }
}