using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.PlateNumber).IsRequired();

        builder.HasOne(v => v.Branch)
               .WithMany(b => b.Vehicles)
               .HasForeignKey(v => v.BranchId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.VehicleType)
               .WithMany(vt => vt.Vehicles)
               .HasForeignKey(v => v.VehicleTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.VehicleBrand)
               .WithMany(vb => vb.Vehicles)
               .HasForeignKey(v => v.VehicleBrandId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.VehicleModel)
               .WithMany(vm => vm.Vehicles)
               .HasForeignKey(v => v.VehicleModelId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.VehicleYear)
               .WithMany(vy => vy.Vehicles)
               .HasForeignKey(v => v.VehicleYearId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.VehicleColor)
               .WithMany(vc => vc.Vehicles)
               .HasForeignKey(v => v.VehicleColorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.KeyLocation)
               .WithMany(kl => kl.Vehicles)
               .HasForeignKey(v => v.KeyLocationId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.LinkingRegion)
               .WithMany(lr => lr.Vehicles)
               .HasForeignKey(v => v.LinkingRegionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.LinkingReason)
               .WithMany(lr => lr.Vehicles)
               .HasForeignKey(v => v.LinkingReasonId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.ParkLocation)
               .WithMany(pl => pl.Vehicles)
               .HasForeignKey(v => v.ParkLocationId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(v => v.EnforcementRecords)
               .WithOne(er => er.Vehicle)
               .HasForeignKey(er => er.VehicleId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Images)
               .WithOne(img => img.Vehicle)
               .HasForeignKey(img => img.VehicleId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Documents)
               .WithOne(doc => doc.Vehicle)
               .HasForeignKey(doc => doc.VehicleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
