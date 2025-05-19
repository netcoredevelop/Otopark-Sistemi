using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.LinkingRegion;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "LinkingRegion_View", "LinkingRegion_Add", "LinkingRegion_Edit", "LinkingRegion_Remove")]
public class LinkingRegionController : Controller
{
    private readonly ILinkingRegionService _linkingRegionService;

    public LinkingRegionController(ILinkingRegionService linkingRegionService)
    {
        _linkingRegionService = linkingRegionService;
    }

    // GET: LinkingRegion
    [RoleAuthorization("Admin", "LinkingRegion_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var linkingRegions = await _linkingRegionService.GetAllAsync();
            return View(linkingRegions);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı bölgeleri listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: LinkingRegion/Details/5
    [RoleAuthorization("Admin", "LinkingRegion_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var linkingRegion = await _linkingRegionService.GetByIdAsync(id);
            if (linkingRegion == null)
            {
                TempData["ErrorMessage"] = "Bağlantı bölgesi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(linkingRegion);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı bölgesi detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: LinkingRegion/Create
    [RoleAuthorization("Admin", "LinkingRegion_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: LinkingRegion/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "LinkingRegion_Add")]
    public async Task<IActionResult> Create(LinkingRegionCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var linkingRegion = new LinkingRegion
                {
                    Name = model.Name
                };

                await _linkingRegionService.AddAsync(linkingRegion);
                TempData["SuccessMessage"] = "Bağlantı bölgesi başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı bölgesi oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: LinkingRegion/Edit/5
    [RoleAuthorization("Admin", "LinkingRegion_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var linkingRegion = await _linkingRegionService.GetByIdAsync(id);
            if (linkingRegion == null)
            {
                TempData["ErrorMessage"] = "Bağlantı bölgesi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new LinkingRegionEditViewModel
            {
                Id = linkingRegion.Id,
                Name = linkingRegion.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı bölgesi düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: LinkingRegion/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "LinkingRegion_Edit")]
    public async Task<IActionResult> Edit(int id, LinkingRegionEditViewModel model)
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
                var linkingRegion = await _linkingRegionService.GetByIdAsync(id);
                if (linkingRegion == null)
                {
                    TempData["ErrorMessage"] = "Bağlantı bölgesi bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                linkingRegion.Name = model.Name;

                await _linkingRegionService.UpdateAsync(linkingRegion);
                TempData["SuccessMessage"] = "Bağlantı bölgesi başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı bölgesi güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: LinkingRegion/Delete/5
    [RoleAuthorization("Admin", "LinkingRegion_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var linkingRegion = await _linkingRegionService.GetByIdAsync(id);
            if (linkingRegion == null)
            {
                TempData["ErrorMessage"] = "Bağlantı bölgesi bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(linkingRegion);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı bölgesi silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: LinkingRegion/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "LinkingRegion_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _linkingRegionService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Bağlantı bölgesi başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Bağlantı bölgesi silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 