using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.VehicleYear;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "VehicleYear_View", "VehicleYear_Add", "VehicleYear_Edit", "VehicleYear_Remove")]
public class VehicleYearController : Controller
{
    private readonly IVehicleYearService _vehicleYearService;

    public VehicleYearController(IVehicleYearService vehicleYearService)
    {
        _vehicleYearService = vehicleYearService;
    }

    // GET: VehicleYear
    [RoleAuthorization("Admin", "VehicleYear_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var vehicleYears = await _vehicleYearService.GetAllAsync();
            return View(vehicleYears);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç yılları listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: VehicleYear/Details/5
    [RoleAuthorization("Admin", "VehicleYear_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var vehicleYear = await _vehicleYearService.GetByIdAsync(id);
            if (vehicleYear == null)
            {
                TempData["ErrorMessage"] = "Araç yılı bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleYear);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç yılı detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: VehicleYear/Create
    [RoleAuthorization("Admin", "VehicleYear_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: VehicleYear/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleYear_Add")]
    public async Task<IActionResult> Create(VehicleYearCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var vehicleYear = new VehicleYear
                {
                    Name = model.Name
                };

                await _vehicleYearService.AddAsync(vehicleYear);
                TempData["SuccessMessage"] = "Araç yılı başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç yılı oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: VehicleYear/Edit/5
    [RoleAuthorization("Admin", "VehicleYear_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var vehicleYear = await _vehicleYearService.GetByIdAsync(id);
            if (vehicleYear == null)
            {
                TempData["ErrorMessage"] = "Araç yılı bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new VehicleYearEditViewModel
            {
                Id = vehicleYear.Id,
                Name = vehicleYear.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç yılı düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: VehicleYear/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleYear_Edit")]
    public async Task<IActionResult> Edit(int id, VehicleYearEditViewModel model)
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
                var vehicleYear = await _vehicleYearService.GetByIdAsync(id);
                if (vehicleYear == null)
                {
                    TempData["ErrorMessage"] = "Araç yılı bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                vehicleYear.Name = model.Name;

                await _vehicleYearService.UpdateAsync(vehicleYear);
                TempData["SuccessMessage"] = "Araç yılı başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç yılı güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: VehicleYear/Delete/5
    [RoleAuthorization("Admin", "VehicleYear_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var vehicleYear = await _vehicleYearService.GetByIdAsync(id);
            if (vehicleYear == null)
            {
                TempData["ErrorMessage"] = "Araç yılı bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(vehicleYear);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç yılı silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: VehicleYear/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleYear_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _vehicleYearService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Araç yılı başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç yılı silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 