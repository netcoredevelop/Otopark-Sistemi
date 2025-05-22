using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TransactionCategoryConfiguration : IEntityTypeConfiguration<TransactionCategory>
{
    public void Configure(EntityTypeBuilder<TransactionCategory> builder)
    {
        builder.HasKey(tc => tc.Id);
        
        builder.Property(tc => tc.Name)
               .IsRequired()
               .HasMaxLength(100);
               
        builder.Property(tc => tc.Description)
               .HasMaxLength(500);
               
        builder.Property(tc => tc.Type)
               .IsRequired();
               
        builder.HasMany(tc => tc.Transactions)
               .WithOne(t => t.TransactionCategory)
               .HasForeignKey(t => t.TransactionCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
} 