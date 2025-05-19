using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class VehicleTypeService : IVehicleTypeService
{
    private readonly IVehicleTypeRepository _vehicleTypeRepository;

    public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository)
    {
        _vehicleTypeRepository = vehicleTypeRepository;
    }

    public async Task<VehicleType?> GetByIdAsync(int id)
        => await _vehicleTypeRepository.GetByIdAsync(id);

    public async Task<IEnumerable<VehicleType>> GetAllAsync()
        => await _vehicleTypeRepository.GetAllAsync();

    public async Task AddAsync(VehicleType vehicleType)
    {
        await _vehicleTypeRepository.AddAsync(vehicleType);
        await _vehicleTypeRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleType vehicleType)
    {
        _vehicleTypeRepository.Update(vehicleType);
        await _vehicleTypeRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleType = await _vehicleTypeRepository.GetByIdAsync(id);
        if (vehicleType != null)
        {
            _vehicleTypeRepository.Remove(vehicleType);
            await _vehicleTypeRepository.SaveChangesAsync();
        }
    }
} 