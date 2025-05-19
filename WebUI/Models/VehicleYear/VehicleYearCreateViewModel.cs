using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.VehicleYear;

public class VehicleYearCreateViewModel
{
    [Required(ErrorMessage = "Yıl adı gereklidir.")]
    [Display(Name = "Yıl")]
    [StringLength(100, ErrorMessage = "Yıl adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 