using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string? UserName
    {
        get
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                    return null;

                return user.FindFirst(ClaimTypes.Name)?.Value 
                    ?? user.FindFirst(ClaimTypes.Email)?.Value 
                    ?? user.Identity.Name;
            }
            catch
            {
                return null;
            }
        }
    }

    public int? UserId
    {
        get
        {
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                    return null;

                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return null;

                return int.TryParse(userIdClaim, out var userId) ? userId : null;
            }
            catch
            {
                return null;
            }
        }
    }
} 