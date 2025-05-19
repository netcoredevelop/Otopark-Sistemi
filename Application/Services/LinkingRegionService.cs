using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class LinkingRegionService : ILinkingRegionService
{
    private readonly ILinkingRegionRepository _linkingRegionRepository;

    public LinkingRegionService(ILinkingRegionRepository linkingRegionRepository)
    {
        _linkingRegionRepository = linkingRegionRepository;
    }

    public async Task<LinkingRegion?> GetByIdAsync(int id)
        => await _linkingRegionRepository.GetByIdAsync(id);

    public async Task<IEnumerable<LinkingRegion>> GetAllAsync()
        => await _linkingRegionRepository.GetAllAsync();

    public async Task AddAsync(LinkingRegion linkingRegion)
    {
        await _linkingRegionRepository.AddAsync(linkingRegion);
        await _linkingRegionRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(LinkingRegion linkingRegion)
    {
        _linkingRegionRepository.Update(linkingRegion);
        await _linkingRegionRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var linkingRegion = await _linkingRegionRepository.GetByIdAsync(id);
        if (linkingRegion != null)
        {
            _linkingRegionRepository.Remove(linkingRegion);
            await _linkingRegionRepository.SaveChangesAsync();
        }
    }
} 