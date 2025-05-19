using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUI.Auth
{
    public class AuthHelper
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPasswordService _passwordService;

        public AuthHelper(
            IUserService userService, 
            IRoleService roleService,
            IPasswordService passwordService)
        {
            _userService = userService;
            _roleService = roleService;
            _passwordService = passwordService;
        }

        public async Task<bool> ValidateUserAsync(
            string username, 
            string password, 
            HttpContext httpContext,
            bool rememberMe = false)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            
            if (user == null || !_passwordService.VerifyPassword(password, user.PasswordHash))
                return false;

            var claims = await GenerateUserClaimsAsync(user);
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe ? DateTimeOffset.UtcNow.AddDays(7) : null
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }

        private async Task<List<Claim>> GenerateUserClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("FullName", $"{user.FirstName} {user.LastName}")
            };

            var userRoles = await _roleService.GetUserRolesAsync(user.Id);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            return claims;
        }
    }
} 