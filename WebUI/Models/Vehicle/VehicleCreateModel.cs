using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Vehicle
{
    public class VehicleCreateModel
    {
        // ... existing code ...

        [Display(Name = "Satışa Çıktı")]
        public bool IsForSale { get; set; }

        [Display(Name = "Satışa Çıkış Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? SaleDate { get; set; }

        [Display(Name = "Satış Fiyatı")]
        [DataType(DataType.Currency)]
        public decimal? SalePrice { get; set; }
    }
} 