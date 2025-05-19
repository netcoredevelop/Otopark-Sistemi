using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class VehicleColorService : IVehicleColorService
{
    private readonly IVehicleColorRepository _vehicleColorRepository;

    public VehicleColorService(IVehicleColorRepository vehicleColorRepository)
    {
        _vehicleColorRepository = vehicleColorRepository;
    }

    public async Task<VehicleColor?> GetByIdAsync(int id)
        => await _vehicleColorRepository.GetByIdAsync(id);

    public async Task<IEnumerable<VehicleColor>> GetAllAsync()
        => await _vehicleColorRepository.GetAllAsync();

    public async Task AddAsync(VehicleColor vehicleColor)
    {
        await _vehicleColorRepository.AddAsync(vehicleColor);
        await _vehicleColorRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleColor vehicleColor)
    {
        _vehicleColorRepository.Update(vehicleColor);
        await _vehicleColorRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleColor = await _vehicleColorRepository.GetByIdAsync(id);
        if (vehicleColor != null)
        {
            _vehicleColorRepository.Remove(vehicleColor);
            await _vehicleColorRepository.SaveChangesAsync();
        }
    }
} 