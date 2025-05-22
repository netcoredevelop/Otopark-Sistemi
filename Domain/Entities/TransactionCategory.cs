using System.Collections.Generic;

namespace Domain.Entities;

public class TransactionCategory : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TransactionType Type { get; set; } // Income (Gelir) veya Expense (Gider)
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

public enum TransactionType
{
    Gelir = 1,
    Gider = 2
} 