using Domain.Entities;

public class EnforcementRecord:BaseEntity<int>
{
    public DateTime RecordDate { get; set; }
    public string DossierNumber { get; set; } = default!;

    public int EnforcementOfficeId { get; set; }
    public virtual EnforcementOffice EnforcementOffice { get; set; } = null!;
    
    public int VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}