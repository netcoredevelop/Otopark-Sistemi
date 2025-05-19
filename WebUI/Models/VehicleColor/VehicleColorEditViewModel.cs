using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.VehicleColor;

public class VehicleColorEditViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Renk adı gereklidir.")]
    [Display(Name = "Renk Adı")]
    [StringLength(100, ErrorMessage = "Renk adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 