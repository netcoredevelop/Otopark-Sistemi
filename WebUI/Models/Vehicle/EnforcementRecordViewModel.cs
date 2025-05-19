using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Vehicle;

public class EnforcementRecordViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "İcra Numarası")]
    public string EnforcementNumber { get; set; } = default!;
    
    [Display(Name = "İcra Dairesi")]
    public string EnforcementOfficeName { get; set; } = default!;
    
    [Display(Name = "Karar Tarihi")]
    [DataType(DataType.Date)]
    public DateTime DecisionDate { get; set; }
    
    [Display(Name = "Açıklama")]
    public string? Description { get; set; }
    
    public int VehicleId { get; set; }
    
    public int EnforcementOfficeId { get; set; }
} 