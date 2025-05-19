using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IUserService
{
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate);
} 