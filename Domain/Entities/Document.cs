using Domain.Entities;

public class Document : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string FilePath { get; set; } = default!;

    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}