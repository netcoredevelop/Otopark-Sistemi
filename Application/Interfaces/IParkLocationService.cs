using Domain.Entities;

namespace Application.Interfaces;

public interface IParkLocationService
{
    Task<ParkLocation?> GetByIdAsync(int id);
    Task<IEnumerable<ParkLocation>> GetAllAsync();
    Task AddAsync(ParkLocation parkLocation);
    Task UpdateAsync(ParkLocation parkLocation);
    Task DeleteAsync(int id);
} 