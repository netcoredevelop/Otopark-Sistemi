using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.VehicleColor;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "VehicleColor_View", "VehicleColor_Add", "VehicleColor_Edit", "VehicleColor_Remove")]
public class VehicleColorController : Controller
{
    private readonly IVehicleColorService _vehicleColorService;

    public VehicleColorController(IVehicleColorService vehicleColorService)
    {
        _vehicleColorService = vehicleColorService;
    }

    // GET: VehicleColor
    [RoleAuthorization("Admin", "VehicleColor_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var vehicleColors = await _vehicleColorService.GetAllAsync();
            return View(vehicleColors);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç renkleri listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: VehicleColor/Details/5
    [RoleAuthorization("Admin", "VehicleColor_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var vehicleColor = await _vehicleColorService.GetByIdAsync(id);
            if (vehicleColor == null)
            {
                TempData["ErrorMessage"] = "Araç rengi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleColor);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç rengi detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: VehicleColor/Create
    [RoleAuthorization("Admin", "VehicleColor_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: VehicleColor/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleColor_Add")]
    public async Task<IActionResult> Create(VehicleColorCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var vehicleColor = new VehicleColor
                {
                    Name = model.Name
                };

                await _vehicleColorService.AddAsync(vehicleColor);
                TempData["SuccessMessage"] = "Araç rengi başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç rengi oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: VehicleColor/Edit/5
    [RoleAuthorization("Admin", "VehicleColor_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var vehicleColor = await _vehicleColorService.GetByIdAsync(id);
            if (vehicleColor == null)
            {
                TempData["ErrorMessage"] = "Araç rengi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new VehicleColorEditViewModel
            {
                Id = vehicleColor.Id,
                Name = vehicleColor.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç rengi düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: VehicleColor/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleColor_Edit")]
    public async Task<IActionResult> Edit(int id, VehicleColorEditViewModel model)
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
                var vehicleColor = await _vehicleColorService.GetByIdAsync(id);
                if (vehicleColor == null)
                {
                    TempData["ErrorMessage"] = "Araç rengi bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                vehicleColor.Name = model.Name;

                await _vehicleColorService.UpdateAsync(vehicleColor);
                TempData["SuccessMessage"] = "Araç rengi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç rengi güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: VehicleColor/Delete/5
    [RoleAuthorization("Admin", "VehicleColor_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var vehicleColor = await _vehicleColorService.GetByIdAsync(id);
            if (vehicleColor == null)
            {
                TempData["ErrorMessage"] = "Araç rengi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(vehicleColor);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç rengi silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: VehicleColor/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleColor_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _vehicleColorService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Araç rengi başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç rengi silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 