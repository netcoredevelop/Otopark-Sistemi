using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Branch;

public class BranchEditViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Şube adı gereklidir.")]
    [Display(Name = "Şube Adı")]
    [StringLength(100, ErrorMessage = "Şube adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 