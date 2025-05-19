using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class LinkingReasonService : ILinkingReasonService
{
    private readonly ILinkingReasonRepository _linkingReasonRepository;

    public LinkingReasonService(ILinkingReasonRepository linkingReasonRepository)
    {
        _linkingReasonRepository = linkingReasonRepository;
    }

    public async Task<LinkingReason?> GetByIdAsync(int id)
        => await _linkingReasonRepository.GetByIdAsync(id);

    public async Task<IEnumerable<LinkingReason>> GetAllAsync()
        => await _linkingReasonRepository.GetAllAsync();

    public async Task AddAsync(LinkingReason linkingReason)
    {
        await _linkingReasonRepository.AddAsync(linkingReason);
        await _linkingReasonRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(LinkingReason linkingReason)
    {
        _linkingReasonRepository.Update(linkingReason);
        await _linkingReasonRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var linkingReason = await _linkingReasonRepository.GetByIdAsync(id);
        if (linkingReason != null)
        {
            _linkingReasonRepository.Remove(linkingReason);
            await _linkingReasonRepository.SaveChangesAsync();
        }
    }
} 