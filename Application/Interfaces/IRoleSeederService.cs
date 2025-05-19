using Domain.Entities;

namespace Application.Interfaces;

public interface IRoleSeederService
{
    Task SeedRolesAsync();
    Task<List<Role>> GetAllRolesAsync();
} 