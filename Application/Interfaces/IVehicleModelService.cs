using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleModelService
{
    Task<VehicleModel?> GetByIdAsync(int id);
    Task<IEnumerable<VehicleModel>> GetAllAsync();
    Task AddAsync(VehicleModel vehicleModel);
    Task UpdateAsync(VehicleModel vehicleModel);
    Task DeleteAsync(int id);
} 