using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebUI.Auth;
using WebUI.Models.VehicleBrand;

namespace WebUI.Controllers
{
    [RoleAuthorization("Admin", "VehicleBrand_View", "VehicleBrand_Add", "VehicleBrand_Edit", "VehicleBrand_Remove")]
    public class VehicleBrandController : Controller
    {
        private readonly IVehicleBrandService _vehicleBrandService;

        public VehicleBrandController(IVehicleBrandService vehicleBrandService)
        {
            _vehicleBrandService = vehicleBrandService;
        }

        // GET: VehicleBrand
        [RoleAuthorization("Admin", "VehicleBrand_View")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var vehicleBrands = await _vehicleBrandService.GetAllAsync();
                return View(vehicleBrands);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Araç markaları listelenirken bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: VehicleBrand/Details/5
        [RoleAuthorization("Admin", "VehicleBrand_View")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var vehicleBrand = await _vehicleBrandService.GetByIdAsync(id);
                if (vehicleBrand == null)
                {
                    TempData["ErrorMessage"] = "Araç markası bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }
                return View(vehicleBrand);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Araç markası detayları görüntülenirken bir hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: VehicleBrand/Create
        [RoleAuthorization("Admin", "VehicleBrand_Add")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleBrand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleAuthorization("Admin", "VehicleBrand_Add")]
        public async Task<IActionResult> Create(VehicleBrandCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vehicleBrand = new VehicleBrand
                    {
                        Name = model.Name
                    };

                    await _vehicleBrandService.AddAsync(vehicleBrand);
                    TempData["SuccessMessage"] = "Araç markası başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Araç markası oluşturulurken bir hata oluştu: {ex.Message}";
                return View(model);
            }
        }

        // GET: VehicleBrand/Edit/5
        [RoleAuthorization("Admin", "VehicleBrand_Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var vehicleBrand = await _vehicleBrandService.GetByIdAsync(id);
                if (vehicleBrand == null)
                {
                    TempData["ErrorMessage"] = "Araç markası bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                var model = new VehicleBrandEditViewModel
                {
                    Id = vehicleBrand.Id,
                    Name = vehicleBrand.Name
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Araç markası düzenleme sayfası açılırken bir hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: VehicleBrand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleAuthorization("Admin", "VehicleBrand_Edit")]
        public async Task<IActionResult> Edit(int id, VehicleBrandEditViewModel model)
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
                    var vehicleBrand = await _vehicleBrandService.GetByIdAsync(id);
                    if (vehicleBrand == null)
                    {
                        TempData["ErrorMessage"] = "Araç markası bulunamadı.";
                        return RedirectToAction(nameof(Index));
                    }

                    vehicleBrand.Name = model.Name;

                    await _vehicleBrandService.UpdateAsync(vehicleBrand);
                    TempData["SuccessMessage"] = "Araç markası başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Araç markası güncellenirken bir hata oluştu: {ex.Message}";
                return View(model);
            }
        }

        // GET: VehicleBrand/Delete/5
        [RoleAuthorization("Admin", "VehicleBrand_Remove")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var vehicleBrand = await _vehicleBrandService.GetByIdAsync(id);
                if (vehicleBrand == null)
                {
                    TempData["ErrorMessage"] = "Araç markası bulunamadı.";
                    return RedirectToAction(nameof(Index));
                }

                return View(vehicleBrand);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Araç markası silme sayfası açılırken bir hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: VehicleBrand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleAuthorization("Admin", "VehicleBrand_Remove")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _vehicleBrandService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Araç markası başarıyla silindi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Araç markası silinirken bir hata oluştu: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 