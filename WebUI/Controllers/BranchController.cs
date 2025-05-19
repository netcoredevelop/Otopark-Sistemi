using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.Branch;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "Branch_View", "Branch_Add", "Branch_Edit", "Branch_Remove")]
public class BranchController : Controller
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    // GET: Branch
    [RoleAuthorization("Admin", "Branch_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var branches = await _branchService.GetAllAsync();
            return View(branches);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Şubeler listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: Branch/Details/5
    [RoleAuthorization("Admin", "Branch_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null)
            {
                TempData["ErrorMessage"] = "Şube bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Şube detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: Branch/Create
    [RoleAuthorization("Admin", "Branch_Add")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Branch/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "Branch_Add")]
    public async Task<IActionResult> Create(BranchCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var branch = new Branch
                {
                    Name = model.Name
                };

                await _branchService.AddAsync(branch);
                TempData["SuccessMessage"] = "Şube başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Şube oluşturulurken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: Branch/Edit/5
    [RoleAuthorization("Admin", "Branch_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null)
            {
                TempData["ErrorMessage"] = "Şube bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var model = new BranchEditViewModel
            {
                Id = branch.Id,
                Name = branch.Name
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Şube düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Branch/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "Branch_Edit")]
    public async Task<IActionResult> Edit(int id, BranchEditViewModel model)
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
                var branch = await _branchService.GetByIdAsync(id);
                if (branch == null)
                {
                    TempData["ErrorMessage"] = "Şube bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                branch.Name = model.Name;

                await _branchService.UpdateAsync(branch);
                TempData["SuccessMessage"] = "Şube başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Şube güncellenirken bir hata oluştu: {ex.Message}";
            return View(model);
        }
    }

    // GET: Branch/Delete/5
    [RoleAuthorization("Admin", "Branch_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null)
            {
                TempData["ErrorMessage"] = "Şube bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(branch);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Şube silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Branch/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "Branch_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _branchService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Şube başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Şube silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 