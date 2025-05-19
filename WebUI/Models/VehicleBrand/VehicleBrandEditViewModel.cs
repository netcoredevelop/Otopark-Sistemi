using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.VehicleBrand
{
    public class VehicleBrandEditViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Marka adı gereklidir.")]
        [Display(Name = "Marka Adı")]
        [StringLength(100, ErrorMessage = "Marka adı en fazla 100 karakter olabilir.")]
        public string Name { get; set; }
    }
} 