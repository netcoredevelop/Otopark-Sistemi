using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Vehicle;

public class VehicleImageViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Resim Dosyası")]
    public string FilePath { get; set; } = default!;
    
    [Display(Name = "Yüklenme Tarihi")]
    public DateTime UploadedAt { get; set; }
    
    public int VehicleId { get; set; }
} 