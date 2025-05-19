using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.VehicleType;

public class VehicleTypeCreateViewModel
{
    [Required(ErrorMessage = "Tür adı gereklidir.")]
    [Display(Name = "Tür Adı")]
    [StringLength(100, ErrorMessage = "Tür adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 