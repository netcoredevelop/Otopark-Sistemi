using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class LinkingReasonConfiguration : IEntityTypeConfiguration<LinkingReason>
{
    public void Configure(EntityTypeBuilder<LinkingReason> builder)
    {
        builder.HasKey(lr => lr.Id);
        builder.HasMany(lr => lr.Vehicles)
               .WithOne(v => v.LinkingReason)
               .HasForeignKey(v => v.LinkingReasonId);
    }
}
