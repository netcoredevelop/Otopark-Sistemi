namespace Domain.Entities;

public class Vehicle : BaseEntity<int>
{
    public string PlateNumber { get; set; } = default!;
    public string? VehicleOnwer { get; set; }
    public long? VehicleOnwerIdentityNumber { get; set; }
    public string?  BookSequenceNo { get; set; }
    public string? LinkingTeamNumber { get; set; }
    public DateTime EntryDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public string? LinkingAdditionalInformation { get; set; }
    public long? MuhammenBedeli { get; set; }
    public string? Description { get; set; }

    public bool IsForSale { get; set; } = false;
    public DateTime? SaleDate { get; set; }
    public decimal? SalePrice { get; set; } 

    public virtual Branch Branch { get; set; } = null!;
    public int BranchId { get; set; }

    public virtual VehicleType VehicleType { get; set; } = null!;
    public int VehicleTypeId { get; set; }

    public virtual VehicleBrand VehicleBrand { get; set; } = null!;
    public int VehicleBrandId { get; set; }

    public virtual VehicleModel VehicleModel { get; set; } = null!;
    public int VehicleModelId { get; set; }

    public virtual VehicleYear VehicleYear { get; set; } = null!;
    public int VehicleYearId { get; set; }

    public virtual VehicleColor VehicleColor { get; set; } = null!;
    public int VehicleColorId { get; set; }

    public virtual KeyLocation KeyLocation { get; set; } = null!;
    public int KeyLocationId { get; set; }

    public virtual LinkingRegion LinkingRegion { get; set; } = null!;
    public int LinkingRegionId { get; set; }

    public virtual LinkingReason LinkingReason { get; set; } = null!;
    public int LinkingReasonId { get; set; }

    public virtual ParkLocation? ParkLocation { get; set; }
    public int? ParkLocationId { get;set; }

    public virtual ICollection<EnforcementRecord> EnforcementRecords { get; set; } = new List<EnforcementRecord>();
    public virtual ICollection<VehicleImage> Images { get; set; } = new List<VehicleImage>();
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
