using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class RoleSeederService : IRoleSeederService
{
    private readonly IRoleRepository _roleRepository;
    
    // Entity isimleri
    private readonly string[] _entityNames = new[] 
    { 
        "Vehicle", "VehicleBrand", "VehicleModel", "VehicleType", "VehicleColor", 
        "VehicleYear", "KeyLocation", "LinkingReason", "LinkingRegion", "Branch", 
        "ParkLocation", "VehicleImage", "Document", "EnforcementRecord", "EnforcementOffice", 
        "User", "Role"
    };
    
    // İşlem isimleri
    private readonly string[] _actions = new[] { "Add", "Edit", "Remove", "View" };

    public RoleSeederService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task SeedRolesAsync()
    {
        // Admin rolünü ekleyelim
        var adminRole = await _roleRepository.FindAsync(r => r.Name == "Admin");
        if (!adminRole.Any())
        {
            await _roleRepository.AddAsync(new Role { Name = "Admin" });
            await _roleRepository.SaveChangesAsync();
        }

        // User rolünü ekleyelim
        var userRole = await _roleRepository.FindAsync(r => r.Name == "User");
        if (!userRole.Any())
        {
            await _roleRepository.AddAsync(new Role { Name = "User" });
            await _roleRepository.SaveChangesAsync();
        }

        // Entity ve eylem bazlı yetkileri oluşturalım
        foreach (var entityName in _entityNames)
        {
            foreach (var action in _actions)
            {
                string roleName = $"{entityName}_{action}";
                var existingRole = await _roleRepository.FindAsync(r => r.Name == roleName);
                if (!existingRole.Any())
                {
                    await _roleRepository.AddAsync(new Role { Name = roleName });
                    await _roleRepository.SaveChangesAsync();
                }
            }
        }
    }

    public async Task<List<Role>> GetAllRolesAsync()
    {
        var allRoles = (await _roleRepository.GetAllAsync()).ToList();
        return allRoles.Where(r => !r.Name.StartsWith("UserRole_")).ToList();
    }
} 