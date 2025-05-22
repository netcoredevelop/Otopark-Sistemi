using System.Collections.Generic;

namespace Domain.Entities;

public class Branch:BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public long? Capacity { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public virtual ICollection<Transaction> Transactions { get; set; }
}
