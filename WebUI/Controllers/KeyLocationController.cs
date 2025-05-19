using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.KeyLocation;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "KeyLocation_View", "KeyLocation_Add", "KeyLocation_Edit", "KeyLocation_Remove")]
public class KeyLocationController : Controller
{
    private readonly IKeyLocationService _keyLocationService;

    public KeyLocationController(IKeyLocationService keyLocationService)
    {
        _keyLocationService = keyLocationService;
    }

    // GET: KeyLocation
    [RoleAuthorization("Admin", "KeyLocation_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var keyLocations = await _keyLocationService.GetAllAsync();
            return View(keyLocations);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Anahtar yerleri listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: KeyLocation/Details/5
    [RoleAuthorization("Admin", "KeyLocation_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var keyLocation = await _keyLocationService.GetByIdAsync(id);
            if (keyLocation == null)
            {
                TempData["ErrorMessage"] = "Anahtar yeri bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(keyLocation);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Anahtar yeri detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: KeyLocation/Create
    [RoleAuthorization("Admin", "KeyLocation_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: KeyLocation/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "KeyLocation_Add")]
    public async Task<IActionResult> Create(KeyLocationCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var keyLocation = new KeyLocation
                {
                    Name = model.Name
                };

                await _keyLocationService.AddAsync(keyLocation);
                TempData["SuccessMessage"] = "Anahtar yeri başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Anahtar yeri oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: KeyLocation/Edit/5
    [RoleAuthorization("Admin", "KeyLocation_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var keyLocation = await _keyLocationService.GetByIdAsync(id);
            if (keyLocation == null)
            {
                TempData["ErrorMessage"] = "Anahtar yeri bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new KeyLocationEditViewModel
            {
                Id = keyLocation.Id,
                Name = keyLocation.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Anahtar yeri düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: KeyLocation/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "KeyLocation_Edit")]
    public async Task<IActionResult> Edit(int id, KeyLocationEditViewModel model)
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
                var keyLocation = await _keyLocationService.GetByIdAsync(id);
                if (keyLocation == null)
                {
                    TempData["ErrorMessage"] = "Anahtar yeri bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                keyLocation.Name = model.Name;

                await _keyLocationService.UpdateAsync(keyLocation);
                TempData["SuccessMessage"] = "Anahtar yeri başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Anahtar yeri güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: KeyLocation/Delete/5
    [RoleAuthorization("Admin", "KeyLocation_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var keyLocation = await _keyLocationService.GetByIdAsync(id);
            if (keyLocation == null)
            {
                TempData["ErrorMessage"] = "Anahtar yeri bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(keyLocation);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Anahtar yeri silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: KeyLocation/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "KeyLocation_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _keyLocationService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Anahtar yeri başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Anahtar yeri silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 