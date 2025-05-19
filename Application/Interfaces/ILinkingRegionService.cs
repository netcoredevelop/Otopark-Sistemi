using Domain.Entities;

namespace Application.Interfaces;

public interface ILinkingRegionService
{
    Task<LinkingRegion?> GetByIdAsync(int id);
    Task<IEnumerable<LinkingRegion>> GetAllAsync();
    Task AddAsync(LinkingRegion linkingRegion);
    Task UpdateAsync(LinkingRegion linkingRegion);
    Task DeleteAsync(int id);
} 