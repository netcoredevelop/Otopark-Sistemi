using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Models.VehicleModel;

public class VehicleModelCreateViewModel
{
    [Required(ErrorMessage = "Model adı gereklidir.")]
    [Display(Name = "Model Adı")]
    [StringLength(100, ErrorMessage = "Model adı en fazla 100 karakter olabilir.")]
    public string Name { get; set; } = default!;
    
    [Required(ErrorMessage = "Marka seçimi gereklidir.")]
    [Display(Name = "Marka")]
    public int VehicleBrandId { get; set; }
    
    public SelectList? VehicleBrands { get; set; }
} 