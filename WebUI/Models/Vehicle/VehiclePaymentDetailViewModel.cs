using System;

namespace WebUI.Models.Vehicle;

public class VehiclePaymentDetailViewModel
{
    public decimal Amount { get; set; } // Ödediği Toplam Tutar
    public DateTime PaymentDate { get; set; } // Ödemenin Alındığı Tarih
    public int TotalDays { get; set; } // Araçın Kaç Gün Kaldığı
    public string? Description { get; set; } // Açıklama
    public bool IsPay { get; set; } = false;

}
