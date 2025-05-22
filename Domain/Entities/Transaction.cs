using System;

namespace Domain.Entities;

public class Transaction : BaseEntity<int>
{
    public int TransactionCategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = default!;
    public DateTime TransactionDate { get; set; }
    public string? ReferenceNumber { get; set; } = default!; // Fatura/İrsaliye No gibi
    
    // Opsiyonel ilişkiler - bir işlem şubeye, araca veya genel olabilir
    public int? BranchId { get; set; }
    public int? VehicleId { get; set; }
    
    // Navigation properties
    public virtual TransactionCategory TransactionCategory { get; set; } = default!;
    public virtual Branch? Branch { get; set; }
    public virtual Vehicle? Vehicle { get; set; }
} 