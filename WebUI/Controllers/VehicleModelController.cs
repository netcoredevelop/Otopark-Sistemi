using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUI.Auth;
using WebUI.Models.VehicleModel;

namespace WebUI.Controllers;

[RoleAuthorization("Admin", "VehicleModel_View", "VehicleModel_Add", "VehicleModel_Edit", "VehicleModel_Remove")]
public class VehicleModelController : Controller
{
    private readonly IVehicleModelService _vehicleModelService;
    private readonly IVehicleBrandService _vehicleBrandService;

    public VehicleModelController(IVehicleModelService vehicleModelService, IVehicleBrandService vehicleBrandService)
    {
        _vehicleModelService = vehicleModelService;
        _vehicleBrandService = vehicleBrandService;
    }

    // GET: VehicleModel
    [RoleAuthorization("Admin", "VehicleModel_View")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var vehicleModels = await _vehicleModelService.GetAllAsync();
            return View(vehicleModels);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç modelleri listelenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction("Index", "Home");
        }
    }

    // GET: VehicleModel/Details/5
    [RoleAuthorization("Admin", "VehicleModel_View")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var vehicleModel = await _vehicleModelService.GetByIdAsync(id);
            if (vehicleModel == null)
            {
                TempData["ErrorMessage"] = "Araç modeli bulunamadı.";
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç modeli detayları görüntülenirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: VehicleModel/Create
    [RoleAuthorization("Admin", "VehicleModel_Add")]
    public async Task<IActionResult> Create()
    {
        var brands = await _vehicleBrandService.GetAllAsync();
        var model = new VehicleModelCreateViewModel
        {
            VehicleBrands = new SelectList(brands, "Id", "Name")
        };
        return View(model);
    }

    // POST: VehicleModel/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleModel_Add")]
    public async Task<IActionResult> Create(VehicleModelCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // VehicleBrand'i doğrula
                var brand = await _vehicleBrandService.GetByIdAsync(model.VehicleBrandId);
                if (brand == null)
                {
                    ModelState.AddModelError("VehicleBrandId", "Geçerli bir araç markası seçiniz.");
					model.VehicleBrands = new SelectList(await _vehicleBrandService.GetAllAsync(), "Id", "Name", model.VehicleBrandId);
					return View(model);
                }

                var vehicleModel = new VehicleModel
                {
                    Name = model.Name,
                    VehicleBrandId = model.VehicleBrandId,
                    VehicleBrand = brand // Brand referansını ayarla
                };

                await _vehicleModelService.AddAsync(vehicleModel);
                TempData["SuccessMessage"] = "Araç modeli başarıyla oluşturuldu.";
                return RedirectToAction(nameof(Index));
            }
            
            var brands = await _vehicleBrandService.GetAllAsync();
            model.VehicleBrands = new SelectList(brands, "Id", "Name", model.VehicleBrandId);
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç modeli oluşturulurken bir hata oluştu: {ex.Message}";
            model.VehicleBrands = new SelectList(await _vehicleBrandService.GetAllAsync(), "Id", "Name", model.VehicleBrandId);
            return View(model);
        }
    }

    // GET: VehicleModel/Edit/5
    [RoleAuthorization("Admin", "VehicleModel_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var vehicleModel = await _vehicleModelService.GetByIdAsync(id);
            if (vehicleModel == null)
            {
                TempData["ErrorMessage"] = "Araç modeli bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            var brands = await _vehicleBrandService.GetAllAsync();
            var model = new VehicleModelEditViewModel
            {
                Id = vehicleModel.Id,
                Name = vehicleModel.Name,
                VehicleBrandId = vehicleModel.VehicleBrandId,
                VehicleBrands = new SelectList(brands, "Id", "Name", vehicleModel.VehicleBrandId)
            };

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç modeli düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: VehicleModel/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleModel_Edit")]
    public async Task<IActionResult> Edit(int id, VehicleModelEditViewModel model)
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
                var vehicleModel = await _vehicleModelService.GetByIdAsync(id);
                if (vehicleModel == null)
                {
                    TempData["ErrorMessage"] = "Araç modeli bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                vehicleModel.Name = model.Name;
                vehicleModel.VehicleBrandId = model.VehicleBrandId;

                await _vehicleModelService.UpdateAsync(vehicleModel);
                TempData["SuccessMessage"] = "Araç modeli başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            
            var brands = await _vehicleBrandService.GetAllAsync();
            model.VehicleBrands = new SelectList(brands, "Id", "Name", model.VehicleBrandId);
            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç modeli güncellenirken bir hata oluştu: {ex.Message}";
            var brands = await _vehicleBrandService.GetAllAsync();
            model.VehicleBrands = new SelectList(brands, "Id", "Name", model.VehicleBrandId);
            return View(model);
        }
    }

    // GET: VehicleModel/Delete/5
    [RoleAuthorization("Admin", "VehicleModel_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var vehicleModel = await _vehicleModelService.GetByIdAsync(id);
            if (vehicleModel == null)
            {
                TempData["ErrorMessage"] = "Araç modeli bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            return View(vehicleModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç modeli silme sayfası açılırken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: VehicleModel/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization("Admin", "VehicleModel_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _vehicleModelService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Araç modeli başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Araç modeli silinirken bir hata oluştu: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
} 