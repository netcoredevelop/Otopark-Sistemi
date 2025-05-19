using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleImageService
{
    Task<VehicleImage?> GetByIdAsync(int id);
    Task<IEnumerable<VehicleImage>> GetAllAsync();
    Task AddAsync(VehicleImage vehicleImage);
    Task UpdateAsync(VehicleImage vehicleImage);
    Task DeleteAsync(int id);
} 