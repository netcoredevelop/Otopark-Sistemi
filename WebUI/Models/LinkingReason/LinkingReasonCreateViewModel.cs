using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.LinkingReason;

public class LinkingReasonCreateViewModel
{
    [Required(ErrorMessage = "Bağlantı nedeni adı gereklidir.")]
    [Display(Name = "Bağlantı Nedeni")]
    [StringLength(100, ErrorMessage = "Bağlantı nedeni adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 