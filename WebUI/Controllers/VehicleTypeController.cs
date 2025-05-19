using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.VehicleType;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "VehicleType_View", "VehicleType_Add", "VehicleType_Edit", "VehicleType_Remove")]
public class VehicleTypeController : Controller
{
    private readonly IVehicleTypeService _vehicleTypeService;

    public VehicleTypeController(IVehicleTypeService vehicleTypeService)
    {
        _vehicleTypeService = vehicleTypeService;
    }

    // GET: VehicleType
    [RoleAuthorization("Admin", "VehicleType_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var vehicleTypes = await _vehicleTypeService.GetAllAsync();
            return View(vehicleTypes);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç türleri listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: VehicleType/Details/5
    [RoleAuthorization("Admin", "VehicleType_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(id);
            if (vehicleType == null)
            {
                TempData["ErrorMessage"] = "Araç türü bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleType);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç türü detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: VehicleType/Create
    [RoleAuthorization("Admin", "VehicleType_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: VehicleType/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleType_Add")]
    public async Task<IActionResult> Create(VehicleTypeCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var vehicleType = new VehicleType
                {
                    Name = model.Name
                };

                await _vehicleTypeService.AddAsync(vehicleType);
                TempData["SuccessMessage"] = "Araç türü başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç türü oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: VehicleType/Edit/5
    [RoleAuthorization("Admin", "VehicleType_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(id);
            if (vehicleType == null)
            {
                TempData["ErrorMessage"] = "Araç türü bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new VehicleTypeEditViewModel
            {
                Id = vehicleType.Id,
                Name = vehicleType.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç türü düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: VehicleType/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleType_Edit")]
    public async Task<IActionResult> Edit(int id, VehicleTypeEditViewModel model)
    {
        try
        {
            if (id != model.Id)
            {
                TempData["ErrorMessage"] = "Geçersiz ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                var vehicleType = await _vehicleTypeService.GetByIdAsync(id);
                if (vehicleType == null)
                {
                    TempData["ErrorMessage"] = "Araç türü bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                vehicleType.Name = model.Name;

                await _vehicleTypeService.UpdateAsync(vehicleType);
                TempData["SuccessMessage"] = "Araç türü başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç türü güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: VehicleType/Delete/5
    [RoleAuthorization("Admin", "VehicleType_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var vehicleType = await _vehicleTypeService.GetByIdAsync(id);
            if (vehicleType == null)
            {
                TempData["ErrorMessage"] = "Araç türü bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(vehicleType);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç türü silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: VehicleType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleType_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _vehicleTypeService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Araç türü başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç türü silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 