using Domain.Entities;

namespace Application.Interfaces;

public interface IEnforcementRecordService
{
    Task<EnforcementRecord?> GetByIdAsync(int id);
    Task<IEnumerable<EnforcementRecord>> GetAllAsync();
    Task AddAsync(EnforcementRecord enforcementRecord);
    Task UpdateAsync(EnforcementRecord enforcementRecord);
    Task DeleteAsync(int id);
} 