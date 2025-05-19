
namespace Domain.Entities;

public class VehicleBrand:BaseEntity<int>
{
    public string Name { get; set; } = default!;

    public ICollection<VehicleModel> Models { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }

    public VehicleBrand()
    {
        Models=new List<VehicleModel>();
        Vehicles=new List<Vehicle>();

    }
}
