using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using WebUI.Auth;
using WebUI.Models;

namespace WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserRoleService _userRoleService;
    private readonly IRoleService _roleService;
    private readonly IPasswordService _passwordService;
    private readonly AuthHelper _authHelper;

    public AccountController(
        IUserService userService,
        IUserRoleService userRoleService,
        IRoleService roleService,
        IPasswordService passwordService,
        AuthHelper authHelper)
    {
        _userService = userService;
        _userRoleService = userRoleService;
        _roleService = roleService;
        _passwordService = passwordService;
        _authHelper = authHelper;
    }

    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (HttpContext.User.Identity.IsAuthenticated)
        {
            if (returnUrl != null)
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (ModelState.IsValid)
        {
            var validLogin = await _authHelper.ValidateUserAsync(
                model.Username,
                model.Password,
                HttpContext,
                model.RememberMe);

            if (validLogin)
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
}