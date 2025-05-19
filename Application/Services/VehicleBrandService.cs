using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class VehicleBrandService : IVehicleBrandService
{
    private readonly IVehicleBrandRepository _vehicleBrandRepository;

    public VehicleBrandService(IVehicleBrandRepository vehicleBrandRepository)
    {
        _vehicleBrandRepository = vehicleBrandRepository;
    }

    public async Task<VehicleBrand?> GetByIdAsync(int id)
        => await _vehicleBrandRepository.GetByIdAsync(id);

    public async Task<IEnumerable<VehicleBrand>> GetAllAsync()
        => await _vehicleBrandRepository.GetAllAsync();

    public async Task AddAsync(VehicleBrand vehicleBrand)
    {
        await _vehicleBrandRepository.AddAsync(vehicleBrand);
        await _vehicleBrandRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleBrand vehicleBrand)
    {
        _vehicleBrandRepository.Update(vehicleBrand);
        await _vehicleBrandRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleBrand = await _vehicleBrandRepository.GetByIdAsync(id);
        if (vehicleBrand != null)
        {
            _vehicleBrandRepository.Remove(vehicleBrand);
            await _vehicleBrandRepository.SaveChangesAsync();
        }
    }
} 