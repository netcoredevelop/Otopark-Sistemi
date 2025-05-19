using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class EnforcementOfficeConfiguration : IEntityTypeConfiguration<EnforcementOffice>
{
    public void Configure(EntityTypeBuilder<EnforcementOffice> builder)
    {
        builder.HasKey(eo => eo.Id);
        builder.HasMany(eo => eo.EnforcementRecords)
               .WithOne(er => er.EnforcementOffice)
               .HasForeignKey(er => er.EnforcementOfficeId);
    }
}