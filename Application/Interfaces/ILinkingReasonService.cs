using Domain.Entities;

namespace Application.Interfaces;

public interface ILinkingReasonService
{
    Task<LinkingReason?> GetByIdAsync(int id);
    Task<IEnumerable<LinkingReason>> GetAllAsync();
    Task AddAsync(LinkingReason linkingReason);
    Task UpdateAsync(LinkingReason linkingReason);
    Task DeleteAsync(int id);
} 