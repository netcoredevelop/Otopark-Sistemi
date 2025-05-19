using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.VehicleYear;

public class VehicleYearEditViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Yıl adı gereklidir.")]
    [Display(Name = "Yıl")]
    [StringLength(100, ErrorMessage = "Yıl adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 