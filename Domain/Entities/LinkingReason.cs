namespace Domain.Entities;

public class LinkingReason:BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
