using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace WebUI.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RoleAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _requiredRoles;

    public RoleAuthorizationAttribute(params string[] roles)
    {
        // Eğer tek bir string gelmişse ve virgül içeriyorsa, virgülle ayır
        if (roles.Length == 1 && roles[0].Contains(','))
        {
            _requiredRoles = roles[0].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(r => r.Trim())
                                   .ToArray();
        }
        else
        {
            _requiredRoles = roles;
        }
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            // Kullanıcı giriş yapmamış, giriş sayfasına yönlendir
            context.Result = new RedirectToActionResult("Login", "Account", null);
            return;
        }

        // Kullanıcının rollerini al
        var userRoleClaims = context.HttpContext.User.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();

        // Admin rolüne sahip kullanıcılar her şeye erişebilir
        if (userRoleClaims.Contains("Admin"))
        {
            return;
        }

        // Kullanıcı gerekli rollerden en az birine sahip mi?
        if (_requiredRoles.Any() && !_requiredRoles.Any(role => userRoleClaims.Contains(role)))
        {
            // Kullanıcı gerekli rollerden hiçbirine sahip değil
            context.Result = new ForbidResult();
        }
    }
} 