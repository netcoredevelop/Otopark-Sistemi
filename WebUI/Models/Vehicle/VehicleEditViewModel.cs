using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Models.Vehicle;

public class VehicleEditViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Plaka numarası gereklidir.")]
    [Display(Name = "Plaka Numarası")]
    [StringLength(20, ErrorMessage = "Plaka numarası en fazla 20 karakter olabilir.")]
    public string PlateNumber { get; set; } = default!;

    [Display(Name = "Araç Sahibi")]
    [StringLength(100, ErrorMessage = "Araç sahibi adı en fazla 100 karakter olabilir.")]
    public string? VehicleOnwer { get; set; }

    [Display(Name = "Araç Sahibi T.C. No")]
    public long? VehicleOnwerIdentityNumber { get; set; }

    [Display(Name = "Defter Sıra No")]
    [StringLength(50, ErrorMessage = "Defter sıra no en fazla 50 karakter olabilir.")]
    public string? BookSequenceNo { get; set; }

    [Display(Name = "Bağlama Ekip No")]
    [StringLength(50, ErrorMessage = "Bağlama ekip no en fazla 50 karakter olabilir.")]
    public string? LinkingTeamNumber { get; set; }

    [Required(ErrorMessage = "Giriş tarihi gereklidir.")]
    [Display(Name = "Giriş Tarihi")]
    [DataType(DataType.Date)]
    public DateTime EntryDate { get; set; }

    [Display(Name = "Çıkış Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? ExitDate { get; set; }

    [Display(Name = "Bağlama Ek Bilgi")]
    [StringLength(500, ErrorMessage = "Bağlama ek bilgisi en fazla 500 karakter olabilir.")]
    public string? LinkingAdditionalInformation { get; set; }

    [Display(Name = "Muhammen Bedeli")]
    public long? MuhammenBedeli { get; set; }

    [Display(Name = "Açıklama")]
    [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Şube seçimi gereklidir.")]
    [Display(Name = "Şube")]
    public int BranchId { get; set; }
    public SelectList? Branches { get; set; }

    [Required(ErrorMessage = "Araç türü seçimi gereklidir.")]
    [Display(Name = "Araç Türü")]
    public int VehicleTypeId { get; set; }
    public SelectList? VehicleTypes { get; set; }

    [Required(ErrorMessage = "Araç markası seçimi gereklidir.")]
    [Display(Name = "Araç Markası")]
    public int VehicleBrandId { get; set; }
    public SelectList? VehicleBrands { get; set; }

    [Required(ErrorMessage = "Araç modeli seçimi gereklidir.")]
    [Display(Name = "Araç Modeli")]
    public int VehicleModelId { get; set; }
    public SelectList? VehicleModels { get; set; }

    [Required(ErrorMessage = "Araç yılı seçimi gereklidir.")]
    [Display(Name = "Araç Yılı")]
    public int VehicleYearId { get; set; }
    public SelectList? VehicleYears { get; set; }

    [Required(ErrorMessage = "Araç rengi seçimi gereklidir.")]
    [Display(Name = "Araç Rengi")]
    public int VehicleColorId { get; set; }
    public SelectList? VehicleColors { get; set; }

    [Required(ErrorMessage = "Anahtar yeri seçimi gereklidir.")]
    [Display(Name = "Anahtar Yeri")]
    public int KeyLocationId { get; set; }
    public SelectList? KeyLocations { get; set; }

    [Required(ErrorMessage = "Bağlantı bölgesi seçimi gereklidir.")]
    [Display(Name = "Bağlantı Bölgesi")]
    public int LinkingRegionId { get; set; }
    public SelectList? LinkingRegions { get; set; }

    [Required(ErrorMessage = "Bağlantı nedeni seçimi gereklidir.")]
    [Display(Name = "Bağlantı Nedeni")]
    public int LinkingReasonId { get; set; }
    public SelectList? LinkingReasons { get; set; }

    [Display(Name = "Park Yeri")]
    public int? ParkLocationId { get; set; }
    public SelectList? ParkLocations { get; set; }
} 