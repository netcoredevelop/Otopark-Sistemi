using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Amount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();
               
        builder.Property(t => t.Description)
               .HasMaxLength(500);
               
        builder.Property(t => t.ReferenceNumber)
               .HasMaxLength(50);
               
        builder.Property(t => t.TransactionDate)
               .IsRequired();
               
        // TransactionCategory ilişkisi
        builder.HasOne(t => t.TransactionCategory)
               .WithMany(c => c.Transactions)
               .HasForeignKey(t => t.TransactionCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
               
        // Branch ilişkisi
        builder.HasOne(t => t.Branch)
               .WithMany(b => b.Transactions)
               .HasForeignKey(t => t.BranchId)
               .OnDelete(DeleteBehavior.Restrict);
               
        // Vehicle ilişkisi (opsiyonel)
        builder.HasOne(t => t.Vehicle)
               .WithMany(v => v.Transactions)
               .HasForeignKey(t => t.VehicleId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);
    }
} 