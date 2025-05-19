using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.EnforcementOffice;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "EnforcementOffice_View", "EnforcementOffice_Add", "EnforcementOffice_Edit", "EnforcementOffice_Remove")]
public class EnforcementOfficeController : Controller
{
    private readonly IEnforcementOfficeService _enforcementOfficeService;

    public EnforcementOfficeController(IEnforcementOfficeService enforcementOfficeService)
    {
        _enforcementOfficeService = enforcementOfficeService;
    }

    // GET: EnforcementOffice
    [RoleAuthorization("Admin", "EnforcementOffice_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var enforcementOffices = await _enforcementOfficeService.GetAllAsync();
            return View(enforcementOffices);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"İcra daireleri listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: EnforcementOffice/Details/5
    [RoleAuthorization("Admin", "EnforcementOffice_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var enforcementOffice = await _enforcementOfficeService.GetByIdAsync(id);
            if (enforcementOffice == null)
            {
                TempData["ErrorMessage"] = "İcra dairesi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(enforcementOffice);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"İcra dairesi detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: EnforcementOffice/Create
    [RoleAuthorization("Admin", "EnforcementOffice_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: EnforcementOffice/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "EnforcementOffice_Add")]
    public async Task<IActionResult> Create(EnforcementOfficeCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var enforcementOffice = new EnforcementOffice
                {
                    Name = model.Name
                };

                await _enforcementOfficeService.AddAsync(enforcementOffice);
                TempData["SuccessMessage"] = "İcra dairesi başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"İcra dairesi oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: EnforcementOffice/Edit/5
    [RoleAuthorization("Admin", "EnforcementOffice_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var enforcementOffice = await _enforcementOfficeService.GetByIdAsync(id);
            if (enforcementOffice == null)
            {
                TempData["ErrorMessage"] = "İcra dairesi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new EnforcementOfficeEditViewModel
            {
                Id = enforcementOffice.Id,
                Name = enforcementOffice.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"İcra dairesi düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: EnforcementOffice/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "EnforcementOffice_Edit")]
    public async Task<IActionResult> Edit(int id, EnforcementOfficeEditViewModel model)
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
                var enforcementOffice = await _enforcementOfficeService.GetByIdAsync(id);
                if (enforcementOffice == null)
                {
                    TempData["ErrorMessage"] = "İcra dairesi bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                enforcementOffice.Name = model.Name;

                await _enforcementOfficeService.UpdateAsync(enforcementOffice);
                TempData["SuccessMessage"] = "İcra dairesi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"İcra dairesi güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: EnforcementOffice/Delete/5
    [RoleAuthorization("Admin", "EnforcementOffice_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var enforcementOffice = await _enforcementOfficeService.GetByIdAsync(id);
            if (enforcementOffice == null)
            {
                TempData["ErrorMessage"] = "İcra dairesi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(enforcementOffice);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"İcra dairesi silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: EnforcementOffice/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "EnforcementOffice_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _enforcementOfficeService.DeleteAsync(id);
            TempData["SuccessMessage"] = "İcra dairesi başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"İcra dairesi silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 