using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ParkLocation;

public class ParkLocationCreateViewModel
{
    [Required(ErrorMessage = "Park yeri adı gereklidir.")]
    [Display(Name = "Park Yeri Adı")]
    [StringLength(100, ErrorMessage = "Park yeri adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 