using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class VehicleModelService : IVehicleModelService
{
    private readonly IVehicleModelRepository _vehicleModelRepository;

    public VehicleModelService(IVehicleModelRepository vehicleModelRepository)
    {
        _vehicleModelRepository = vehicleModelRepository;
    }

    public async Task<VehicleModel?> GetByIdAsync(int id)
        => await _vehicleModelRepository.GetByIdAsync(id);

    public async Task<IEnumerable<VehicleModel>> GetAllAsync()
        => await _vehicleModelRepository.GetAllAsync();

    public async Task AddAsync(VehicleModel vehicleModel)
    {
        await _vehicleModelRepository.AddAsync(vehicleModel);
        await _vehicleModelRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleModel vehicleModel)
    {
        _vehicleModelRepository.Update(vehicleModel);
        await _vehicleModelRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleModel = await _vehicleModelRepository.GetByIdAsync(id);
        if (vehicleModel != null)
        {
            _vehicleModelRepository.Remove(vehicleModel);
            await _vehicleModelRepository.SaveChangesAsync();
        }
    }
} 