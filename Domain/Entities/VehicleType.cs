namespace Domain.Entities;

public class VehicleType:BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public decimal? UkomePrice { get; set; }
    public decimal? PolicePrice { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
