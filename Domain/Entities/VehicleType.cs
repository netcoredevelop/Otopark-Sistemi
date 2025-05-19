namespace Domain.Entities;

public class VehicleType:BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
