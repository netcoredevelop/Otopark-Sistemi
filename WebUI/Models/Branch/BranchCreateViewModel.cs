using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Branch;

public class BranchCreateViewModel
{
    [Required(ErrorMessage = "Şube adı gereklidir.")]
    [Display(Name = "Şube Adı")]
    [StringLength(100, ErrorMessage = "Şube adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 