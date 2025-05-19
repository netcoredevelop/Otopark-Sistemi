using Domain.Entities;

namespace Application.Interfaces;

public interface IRoleService
{
    Task<Role?> GetByIdAsync(int id);
    Task<IEnumerable<Role>> GetAllAsync();
    Task AddAsync(Role role);
    Task UpdateAsync(Role role);
    Task DeleteAsync(int id);
    Task<List<Role>> GetUserRolesAsync(int userId);
} 