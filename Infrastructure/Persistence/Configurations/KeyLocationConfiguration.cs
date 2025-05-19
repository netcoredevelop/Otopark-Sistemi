using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class KeyLocationConfiguration : IEntityTypeConfiguration<KeyLocation>
{
    public void Configure(EntityTypeBuilder<KeyLocation> builder)
    {
        builder.HasKey(kl => kl.Id);
        builder.HasMany(kl => kl.Vehicles)
               .WithOne(v => v.KeyLocation)
               .HasForeignKey(v => v.KeyLocationId);
    }
}
