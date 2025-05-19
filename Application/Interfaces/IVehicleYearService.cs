using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleYearService
{
    Task<VehicleYear?> GetByIdAsync(int id);
    Task<IEnumerable<VehicleYear>> GetAllAsync();
    Task AddAsync(VehicleYear vehicleYear);
    Task UpdateAsync(VehicleYear vehicleYear);
    Task DeleteAsync(int id);
} 