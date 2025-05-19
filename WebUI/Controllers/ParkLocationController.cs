using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.ParkLocation;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "ParkLocation_View", "ParkLocation_Add", "ParkLocation_Edit", "ParkLocation_Remove")]
public class ParkLocationController : Controller
{
    private readonly IParkLocationService _parkLocationService;

    public ParkLocationController(IParkLocationService parkLocationService)
    {
        _parkLocationService = parkLocationService;
    }

    // GET: ParkLocation
    [RoleAuthorization("Admin", "ParkLocation_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var parkLocations = await _parkLocationService.GetAllAsync();
            return View(parkLocations);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Park yerleri listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: ParkLocation/Details/5
    [RoleAuthorization("Admin", "ParkLocation_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var parkLocation = await _parkLocationService.GetByIdAsync(id);
            if (parkLocation == null)
            {
                TempData["ErrorMessage"] = "Park yeri bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(parkLocation);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Park yeri detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: ParkLocation/Create
    [RoleAuthorization("Admin", "ParkLocation_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: ParkLocation/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "ParkLocation_Add")]
    public async Task<IActionResult> Create(ParkLocationCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var parkLocation = new ParkLocation
                {
                    Name = model.Name
                };

                await _parkLocationService.AddAsync(parkLocation);
                TempData["SuccessMessage"] = "Park yeri başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Park yeri oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: ParkLocation/Edit/5
    [RoleAuthorization("Admin", "ParkLocation_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var parkLocation = await _parkLocationService.GetByIdAsync(id);
            if (parkLocation == null)
            {
                TempData["ErrorMessage"] = "Park yeri bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new ParkLocationEditViewModel
            {
                Id = parkLocation.Id,
                Name = parkLocation.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Park yeri düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: ParkLocation/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "ParkLocation_Edit")]
    public async Task<IActionResult> Edit(int id, ParkLocationEditViewModel model)
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
                var parkLocation = await _parkLocationService.GetByIdAsync(id);
                if (parkLocation == null)
                {
                    TempData["ErrorMessage"] = "Park yeri bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                parkLocation.Name = model.Name;

                await _parkLocationService.UpdateAsync(parkLocation);
                TempData["SuccessMessage"] = "Park yeri başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Park yeri güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: ParkLocation/Delete/5
    [RoleAuthorization("Admin", "ParkLocation_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var parkLocation = await _parkLocationService.GetByIdAsync(id);
            if (parkLocation == null)
            {
                TempData["ErrorMessage"] = "Park yeri bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(parkLocation);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Park yeri silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: ParkLocation/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "ParkLocation_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _parkLocationService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Park yeri başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Park yeri silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 