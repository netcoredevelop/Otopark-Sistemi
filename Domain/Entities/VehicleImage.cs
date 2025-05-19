using Domain.Entities;

public class VehicleImage:BaseEntity<int>
{
    public string FilePath { get; set; } = default!;

    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;

    public DateTime UploadedAt { get; set; } = DateTime.Now;
}