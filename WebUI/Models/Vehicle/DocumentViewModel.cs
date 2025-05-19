using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Vehicle;

public class DocumentViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Dosya Adı")]
    public string Name { get; set; } = default!;
    
    [Display(Name = "Dosya Yolu")]
    public string FilePath { get; set; } = default!;
    
    [Display(Name = "Oluşturulma Tarihi")]
    public DateTime CreatedAt { get; set; }
    
    public int VehicleId { get; set; }
} 