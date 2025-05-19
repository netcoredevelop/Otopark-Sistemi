using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleColorService
{
    Task<VehicleColor?> GetByIdAsync(int id);
    Task<IEnumerable<VehicleColor>> GetAllAsync();
    Task AddAsync(VehicleColor vehicleColor);
    Task UpdateAsync(VehicleColor vehicleColor);
    Task DeleteAsync(int id);
}
 