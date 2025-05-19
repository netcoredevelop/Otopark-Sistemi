using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.LinkingRegion;

public class LinkingRegionEditViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Bağlantı bölgesi adı gereklidir.")]
    [Display(Name = "Bağlantı Bölgesi")]
    [StringLength(100, ErrorMessage = "Bağlantı bölgesi adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
} 