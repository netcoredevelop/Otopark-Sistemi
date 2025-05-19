using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ParkLocation;

public class ParkLocationEditViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Park yeri adı gereklidir.")]
    [Display(Name = "Park Yeri Adı")]
    [StringLength(100, ErrorMessage = "Park yeri adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 