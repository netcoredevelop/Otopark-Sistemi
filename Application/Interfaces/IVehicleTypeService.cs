using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleTypeService
{
    Task<VehicleType?> GetByIdAsync(int id);
    Task<IEnumerable<VehicleType>> GetAllAsync();
    Task AddAsync(VehicleType vehicleType);
    Task UpdateAsync(VehicleType vehicleType);
    Task DeleteAsync(int id);
}