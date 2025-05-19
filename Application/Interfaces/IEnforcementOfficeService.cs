using Domain.Entities;

namespace Application.Interfaces;

public interface IEnforcementOfficeService
{
    Task<EnforcementOffice?> GetByIdAsync(int id);
    Task<IEnumerable<EnforcementOffice>> GetAllAsync();
    Task AddAsync(EnforcementOffice enforcementOffice);
    Task UpdateAsync(EnforcementOffice enforcementOffice);
    Task DeleteAsync(int id);
} 