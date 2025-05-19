using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IUserRoleService
{
    Task<UserRole?> GetByIdAsync(int id);
    Task<IEnumerable<UserRole>> GetAllAsync();
    Task AddAsync(UserRole userRole);
    Task UpdateAsync(UserRole userRole);
    Task DeleteAsync(int id);
    Task<IEnumerable<UserRole>> FindAsync(Expression<Func<UserRole, bool>> predicate);
} 