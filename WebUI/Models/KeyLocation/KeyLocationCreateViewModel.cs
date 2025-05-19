using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.KeyLocation;

public class KeyLocationCreateViewModel
{
    [Required(ErrorMessage = "Anahtar yeri adı gereklidir.")]
    [Display(Name = "Anahtar Yeri")]
    [StringLength(100, ErrorMessage = "Anahtar yeri adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 