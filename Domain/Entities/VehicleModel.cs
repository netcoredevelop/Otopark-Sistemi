namespace Domain.Entities;

public class VehicleModel : BaseEntity<int>
{
    public string Name { get; set; } = default!;

    public int VehicleBrandId { get; set; }
    public VehicleBrand VehicleBrand { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }

    public VehicleModel()
    {
        VehicleBrand = new();
        Vehicles = new List<Vehicle>();
    }
}
