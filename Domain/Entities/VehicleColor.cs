namespace Domain.Entities;

public class VehicleColor : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
