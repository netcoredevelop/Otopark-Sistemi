using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Models.Vehicle;

public class EnforcementRecordCreateViewModel
{
    [Required(ErrorMessage = "İcra numarası gereklidir.")]
    [Display(Name = "İcra Numarası")]
    [StringLength(100, ErrorMessage = "İcra numarası en fazla 100 karakter olabilir.")]
    public string EnforcementNumber { get; set; } = default!;
    
    [Required(ErrorMessage = "İcra dairesi seçilmelidir.")]
    [Display(Name = "İcra Dairesi")]
    public int EnforcementOfficeId { get; set; }
    
    [Required(ErrorMessage = "Karar tarihi gereklidir.")]
    [Display(Name = "Karar Tarihi")]
    [DataType(DataType.Date)]
    public DateTime DecisionDate { get; set; }
    
    [Display(Name = "Açıklama")]
    [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
    public string? Description { get; set; }
    
    public int VehicleId { get; set; }
    
    public SelectList? EnforcementOffices { get; set; }
} 