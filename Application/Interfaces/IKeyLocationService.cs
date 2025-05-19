using Domain.Entities;

namespace Application.Interfaces;

public interface IKeyLocationService
{
    Task<KeyLocation?> GetByIdAsync(int id);
    Task<IEnumerable<KeyLocation>> GetAllAsync();
    Task AddAsync(KeyLocation keyLocation);
    Task UpdateAsync(KeyLocation keyLocation);
    Task DeleteAsync(int id);
} 