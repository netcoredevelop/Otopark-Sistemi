using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class VehicleYearService : IVehicleYearService
{
    private readonly IVehicleYearRepository _vehicleYearRepository;

    public VehicleYearService(IVehicleYearRepository vehicleYearRepository)
    {
        _vehicleYearRepository = vehicleYearRepository;
    }

    public async Task<VehicleYear?> GetByIdAsync(int id)
        => await _vehicleYearRepository.GetByIdAsync(id);

    public async Task<IEnumerable<VehicleYear>> GetAllAsync()
        => await _vehicleYearRepository.GetAllAsync();

    public async Task AddAsync(VehicleYear vehicleYear)
    {
        await _vehicleYearRepository.AddAsync(vehicleYear);
        await _vehicleYearRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleYear vehicleYear)
    {
        _vehicleYearRepository.Update(vehicleYear);
        await _vehicleYearRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleYear = await _vehicleYearRepository.GetByIdAsync(id);
        if (vehicleYear != null)
        {
            _vehicleYearRepository.Remove(vehicleYear);
            await _vehicleYearRepository.SaveChangesAsync();
        }
    }
} 