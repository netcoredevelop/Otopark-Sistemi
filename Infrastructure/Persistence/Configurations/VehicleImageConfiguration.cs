using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.HasKey(img => img.Id);
            builder.HasOne(img => img.Vehicle)
                   .WithMany(v => v.Images)
                   .HasForeignKey(img => img.VehicleId);
        }
    }
}
