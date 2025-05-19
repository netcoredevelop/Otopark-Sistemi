using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleBrandService
{
    Task<VehicleBrand?> GetByIdAsync(int id);
    Task<IEnumerable<VehicleBrand>> GetAllAsync();
    Task AddAsync(VehicleBrand vehicleBrand);
    Task UpdateAsync(VehicleBrand vehicleBrand);
    Task DeleteAsync(int id);
} 