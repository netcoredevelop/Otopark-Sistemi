using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebUI.Auth;
using WebUI.Models;

namespace WebUI.Controllers;

[RoleAuthorization]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleSeederService _roleSeederService;
    private readonly IUserRoleService _userRoleService;
    private readonly IPasswordService _passwordService;
    private readonly IRoleRepository _roleRepository;

    public UserController(
        IUserService userService, 
        IRoleSeederService roleSeederService,
        IUserRoleService userRoleService,
        IPasswordService passwordService,
        IRoleRepository roleRepository)
    {
        _userService = userService;
        _roleSeederService = roleSeederService;
        _userRoleService = userRoleService;
        _passwordService = passwordService;
        _roleRepository = roleRepository;
    }

    [RoleAuthorization(
        "Admin",
        "User_View")]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync();
        return View(users);
    }

    [RoleAuthorization(
        "Admin",
        "User_Add")]
    public async Task<IActionResult> Create()
    {
        // Eğer roller henüz oluşturulmadıysa oluşturalım
        await _roleSeederService.SeedRolesAsync();
        
        // View model'e tüm rolleri ekleyelim
        var model = new UserCreateViewModel
        {
            AvailableRoles = await _roleSeederService.GetAllRolesAsync()
        };
        
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization(
        "Admin", 
        "User_Add")]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Kullanıcıyı oluştur
                var user = new User
                {
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = _passwordService.HashPassword(model.Password) // Şifre hashleme eklendi
                };

                await _userService.AddAsync(user);

                // Rol ekleme yetkisi kontrolü
                bool hasRoleManagementPermission = User.IsInRole("Admin") || User.IsInRole("Role_Add");

                // Seçilen rolleri kullanıcıya ekle - sadece rol ekleme yetkisi varsa
                if (hasRoleManagementPermission && model.SelectedRoleIds != null && model.SelectedRoleIds.Any())
                {
                    foreach (var roleId in model.SelectedRoleIds)
                    {
                        await _userRoleService.AddAsync(new UserRole
                        {
                            UserId = user.Id,
                            RoleId = roleId
                        });
                    }
                }
                else if (!hasRoleManagementPermission)
                {
                    // Rol ekleme yetkisi yoksa, sadece basic kullanıcı rolü ekle
                    var userRole = await _roleRepository.FindAsync(r => r.Name == "User");
                    if (userRole.Any())
                    {
                        await _userRoleService.AddAsync(new UserRole
                        {
                            UserId = user.Id,
                            RoleId = userRole.First().Id
                        });
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Kullanıcı oluşturulurken bir hata oluştu: {ex.Message}");
            }
        }

        // Eğer ModelState geçerli değilse, rolleri tekrar yükle
        model.AvailableRoles = await _roleSeederService.GetAllRolesAsync();
        return View(model);
    }

    [RoleAuthorization(
        "Admin", 
        "User_Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Kullanıcının rollerini alalım
        var userRoles = await _userRoleService.FindAsync(ur => ur.UserId == id);
        var allRoles = await _roleSeederService.GetAllRolesAsync();

        var model = new UserEditViewModel
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            SelectedRoleIds = userRoles.Select(ur => ur.RoleId).ToList(),
            AvailableRoles = allRoles
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RoleAuthorization(
        "Admin", 
        "User_Edit")]
    public async Task<IActionResult> Edit(int id, UserEditViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        // Şifre ve şifre onayı alanlarını kontrol et
        if (!string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.ConfirmPassword))
        {
            ModelState.AddModelError("ConfirmPassword", "Şifre onayı gereklidir.");
        }
        else if (string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.ConfirmPassword))
        {
            ModelState.AddModelError("Password", "Şifre gereklidir.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                // Kullanıcı bilgilerini güncelle
                user.Username = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                // Şifre güncelleme (boş değilse)
                if (!string.IsNullOrEmpty(model.Password))
                {
                    user.PasswordHash = _passwordService.HashPassword(model.Password);
                }

                await _userService.UpdateAsync(user);

                // Rol düzenleme yetkisi kontrolü
                bool hasRoleManagementPermission = User.IsInRole("Admin") || User.IsInRole("Role_Edit");

                // Kullanıcının rollerini güncelle - sadece rol düzenleme yetkisi varsa
                if (hasRoleManagementPermission)
                {
                    var existingUserRoles = await _userRoleService.FindAsync(ur => ur.UserId == user.Id);
                    foreach (var userRole in existingUserRoles)
                    {
                        await _userRoleService.DeleteAsync(userRole.Id);
                    }

                    if (model.SelectedRoleIds != null && model.SelectedRoleIds.Any())
                    {
                        foreach (var roleId in model.SelectedRoleIds)
                        {
                            await _userRoleService.AddAsync(new UserRole
                            {
                                UserId = user.Id,
                                RoleId = roleId
                            });
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Kullanıcı güncellenirken bir hata oluştu: {ex.Message}");
            }
        }

        // ModelState geçerli değilse, rolleri tekrar yükle
        model.AvailableRoles = await _roleSeederService.GetAllRolesAsync();
        return View(model);
    }

    [RoleAuthorization(
        "Admin", 
        "User_View")]
    public async Task<IActionResult> Details(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Kullanıcı rollerini alıp view model'e ekle
        var userRoles = await _userRoleService.FindAsync(ur => ur.UserId == id);
        var allRoles = await _roleSeederService.GetAllRolesAsync();
        var userRolesList = allRoles.Where(r => userRoles.Any(ur => ur.RoleId == r.Id)).ToList();

        ViewBag.UserRoles = userRolesList;

        return View(user);
    }

    [RoleAuthorization(
        "Admin", 
        "User_Remove")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RoleAuthorization(
        "Admin", 
        "User_Remove")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            // Önce kullanıcı rollerini sil
            var userRoles = await _userRoleService.FindAsync(ur => ur.UserId == id);
            foreach (var userRole in userRoles)
            {
                await _userRoleService.DeleteAsync(userRole.Id);
            }

            // Sonra kullanıcıyı sil
            await _userService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Kullanıcı silinirken bir hata oluştu: {ex.Message}");
            return View();
        }
    }
} 