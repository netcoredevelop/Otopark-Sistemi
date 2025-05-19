using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class EnforcementRecordConfiguration : IEntityTypeConfiguration<EnforcementRecord>
{
    public void Configure(EntityTypeBuilder<EnforcementRecord> builder)
    {
        builder.HasKey(er => er.Id);
        builder.HasOne(er => er.Vehicle)
               .WithMany(v => v.EnforcementRecords)
               .HasForeignKey(er => er.VehicleId);
    }
}