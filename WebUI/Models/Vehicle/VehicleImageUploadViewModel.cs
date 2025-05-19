using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Vehicle;

public class VehicleImageUploadViewModel
{
    [Required]
    public int VehicleId { get; set; }
} 