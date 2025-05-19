using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.LinkingReason;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "LinkingReason_View", "LinkingReason_Add", "LinkingReason_Edit", "LinkingReason_Remove")]
public class LinkingReasonController : Controller
{
    private readonly ILinkingReasonService _linkingReasonService;

    public LinkingReasonController(ILinkingReasonService linkingReasonService)
    {
        _linkingReasonService = linkingReasonService;
    }

    // GET: LinkingReason
    [RoleAuthorization("Admin", "LinkingReason_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var linkingReasons = await _linkingReasonService.GetAllAsync();
            return View(linkingReasons);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı nedenleri listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: LinkingReason/Details/5
    [RoleAuthorization("Admin", "LinkingReason_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var linkingReason = await _linkingReasonService.GetByIdAsync(id);
            if (linkingReason == null)
            {
                TempData["ErrorMessage"] = "Bağlantı nedeni bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(linkingReason);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı nedeni detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: LinkingReason/Create
    [RoleAuthorization("Admin", "LinkingReason_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: LinkingReason/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "LinkingReason_Add")]
    public async Task<IActionResult> Create(LinkingReasonCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var linkingReason = new LinkingReason
                {
                    Name = model.Name
                };

                await _linkingReasonService.AddAsync(linkingReason);
                TempData["SuccessMessage"] = "Bağlantı nedeni başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı nedeni oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: LinkingReason/Edit/5
    [RoleAuthorization("Admin", "LinkingReason_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var linkingReason = await _linkingReasonService.GetByIdAsync(id);
            if (linkingReason == null)
            {
                TempData["ErrorMessage"] = "Bağlantı nedeni bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new LinkingReasonEditViewModel
            {
                Id = linkingReason.Id,
                Name = linkingReason.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı nedeni düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: LinkingReason/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "LinkingReason_Edit")]
    public async Task<IActionResult> Edit(int id, LinkingReasonEditViewModel model)
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
                var linkingReason = await _linkingReasonService.GetByIdAsync(id);
                if (linkingReason == null)
                {
                    TempData["ErrorMessage"] = "Bağlantı nedeni bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                linkingReason.Name = model.Name;

                await _linkingReasonService.UpdateAsync(linkingReason);
                TempData["SuccessMessage"] = "Bağlantı nedeni başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı nedeni güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: LinkingReason/Delete/5
    [RoleAuthorization("Admin", "LinkingReason_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var linkingReason = await _linkingReasonService.GetByIdAsync(id);
            if (linkingReason == null)
            {
                TempData["ErrorMessage"] = "Bağlantı nedeni bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(linkingReason);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı nedeni silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: LinkingReason/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "LinkingReason_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _linkingReasonService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Bağlantı nedeni başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı nedeni silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 