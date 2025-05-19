using Application.Interfaces;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task<UserRole?> GetByIdAsync(int id)
        => await _userRoleRepository.GetByIdAsync(id);

    public async Task<IEnumerable<UserRole>> GetAllAsync()
        => await _userRoleRepository.GetAllAsync();

    public async Task AddAsync(UserRole userRole)
    {
        await _userRoleRepository.AddAsync(userRole);
        await _userRoleRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserRole userRole)
    {
        _userRoleRepository.Update(userRole);
        await _userRoleRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var userRole = await _userRoleRepository.GetByIdAsync(id);
        if (userRole != null)
        {
            _userRoleRepository.Remove(userRole);
            await _userRoleRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<UserRole>> FindAsync(Expression<Func<UserRole, bool>> predicate)
    {
        return await _userRoleRepository.FindAsync(predicate);
    }
} 