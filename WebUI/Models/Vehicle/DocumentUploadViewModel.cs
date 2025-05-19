using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Vehicle
{
    public class DocumentUploadViewModel
    {
        public int VehicleId { get; set; }
        
        [Required(ErrorMessage = "Lütfen en az bir dosya seçiniz.")]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
} 