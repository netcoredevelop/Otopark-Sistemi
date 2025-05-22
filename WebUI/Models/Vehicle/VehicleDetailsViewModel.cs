using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WebUI.Models.Vehicle;

public class VehicleDetailsViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Plaka Numarası")]
    public string PlateNumber { get; set; } = default!;

    [Display(Name = "Araç Sahibi")]
    public string? VehicleOnwer { get; set; }

    [Display(Name = "Araç Sahibi T.C. No")]
    public long? VehicleOnwerIdentityNumber { get; set; }

    [Display(Name = "Defter Sıra No")]
    public string? BookSequenceNo { get; set; }

    [Display(Name = "Bağlama Ekip No")]
    public string? LinkingTeamNumber { get; set; }

    [Display(Name = "Giriş Tarihi")]
    [DataType(DataType.Date)]
    public DateTime EntryDate { get; set; }

    [Display(Name = "Çıkış Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? ExitDate { get; set; }

    [Display(Name = "Bağlama Ek Bilgi")]
    public string? LinkingAdditionalInformation { get; set; }

    [Display(Name = "Muhammen Bedeli")]
    public long? MuhammenBedeli { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Display(Name = "Şube")]
    public string BranchName { get; set; } = default!;

    [Display(Name = "Araç Türü")]
    public string VehicleTypeName { get; set; } = default!;
    public int? VehicleTypeId { get; set; }

    [Display(Name = "Araç Markası")]
    public string VehicleBrandName { get; set; } = default!;

    [Display(Name = "Araç Modeli")]
    public string VehicleModelName { get; set; } = default!;

    [Display(Name = "Araç Yılı")]
    public string VehicleYearName { get; set; } = default!;

    [Display(Name = "Araç Rengi")]
    public string VehicleColorName { get; set; } = default!;

    [Display(Name = "Anahtar Yeri")]
    public string KeyLocationName { get; set; } = default!;

    [Display(Name = "Bağlantı Bölgesi")]
    public string LinkingRegionName { get; set; } = default!;

    [Display(Name = "Bağlantı Nedeni")]
    public string LinkingReasonName { get; set; } = default!;

    [Display(Name = "Park Yeri")]
    public string? ParkLocationName { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public List<DocumentViewModel>? Documents { get; set; }
    public List<VehicleImageViewModel>? Images { get; set; }
    public List<EnforcementRecordViewModel>? EnforcementRecords { get; set; }
    public VehiclePaymentDetailViewModel? PaymentDetail { get; set; }
    public TowingExpenseViewModel TowingExpense { get; set; }

    [Display(Name = "Satışa Çıktı")]
    public bool IsForSale { get; set; }

    [Display(Name = "Satışa Çıkış Tarihi")]
    [DataType(DataType.Date)]
    public DateTime? SaleDate { get; set; }

    [Display(Name = "Satış Fiyatı")]
    [DataType(DataType.Currency)]
    public decimal? SalePrice { get; set; }
} 