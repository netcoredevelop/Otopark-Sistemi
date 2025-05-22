using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;

namespace WebUI.Controllers;

[RoleAuthorization("Admin")]
[ApiController]
[Route("api/[controller]")]
public class PricingController : ControllerBase
{
    private readonly IVehicleTypeService _vehicleTypeService;

   
    public PricingController(IVehicleTypeService vehicleTypeService)
    {
        _vehicleTypeService = vehicleTypeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePricing([FromBody] PricingRequest request)
    {
        try
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(request.VehicleTypeId);
            if (vehicleType == null)
                return NotFound("Araç tipi bulunamadı");

            vehicleType.UkomePrice = request.UkomePrice;
            vehicleType.PolicePrice = request.PolicePrice;

            await _vehicleTypeService.UpdateAsync(vehicleType);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePricing([FromBody] PricingRequest request)
    {
        try
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(request.VehicleTypeId);
            if (vehicleType == null)
                return NotFound("Araç tipi bulunamadı");

            vehicleType.UkomePrice = request.UkomePrice;
            vehicleType.PolicePrice = request.PolicePrice;

            await _vehicleTypeService.UpdateAsync(vehicleType);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePricing(int id)
    {
        try
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(id);
            if (vehicleType == null)
                return NotFound("Araç tipi bulunamadı");

            vehicleType.UkomePrice = null;
            vehicleType.PolicePrice = null;

            await _vehicleTypeService.UpdateAsync(vehicleType);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class PricingRequest
{
    public int VehicleTypeId { get; set; }
    public decimal UkomePrice { get; set; }
    public decimal PolicePrice { get; set; }
} 