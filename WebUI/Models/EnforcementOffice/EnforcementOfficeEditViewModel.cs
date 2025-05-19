using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.EnforcementOffice;

public class EnforcementOfficeEditViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "İcra dairesi adı gereklidir.")]
    [Display(Name = "İcra Dairesi Adı")]
    [StringLength(100, ErrorMessage = "İcra dairesi adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 