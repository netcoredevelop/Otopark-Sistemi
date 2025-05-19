using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
{
    public void Configure(EntityTypeBuilder<VehicleModel> builder)
    {
        builder.HasKey(vm => vm.Id);
        builder.HasMany(vm => vm.Vehicles)
               .WithOne(v => v.VehicleModel)
               .HasForeignKey(v => v.VehicleModelId);

    }
}