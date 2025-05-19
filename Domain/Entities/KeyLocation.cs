namespace Domain.Entities;

public class KeyLocation:BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
