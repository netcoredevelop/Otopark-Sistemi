using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public RoleService(
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository)
    {
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<Role?> GetByIdAsync(int id)
        => await _roleRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Role>> GetAllAsync()
        => await _roleRepository.GetAllAsync();

    public async Task AddAsync(Role role)
    {
        await _roleRepository.AddAsync(role);
        await _roleRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role role)
    {
        _roleRepository.Update(role);
        await _roleRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role != null)
        {
            _roleRepository.Remove(role);
            await _roleRepository.SaveChangesAsync();
        }
    }

    public async Task<List<Role>> GetUserRolesAsync(int userId)
    {
        // Kullanıcının rol eşleştirmelerini al
        var userRoles = await _userRoleRepository.FindAsync(ur => ur.UserId == userId);
        
        // Kullanıcının sahip olduğu rol ID'lerini al
        var roleIds = userRoles.Select(ur => ur.RoleId);
        
        // Her bir rol ID'si için rol nesnesini getir
        var roles = new List<Role>();
        foreach (var roleId in roleIds)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role != null)
            {
                roles.Add(role);
            }
        }
        
        return roles;
    }
} 