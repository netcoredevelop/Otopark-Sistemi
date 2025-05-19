using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class VehicleImageService : IVehicleImageService
{
    private readonly IVehicleImageRepository _vehicleImageRepository;

    public VehicleImageService(IVehicleImageRepository vehicleImageRepository)
    {
        _vehicleImageRepository = vehicleImageRepository;
    }

    public async Task<VehicleImage?> GetByIdAsync(int id)
        => await _vehicleImageRepository.GetByIdAsync(id);

    public async Task<IEnumerable<VehicleImage>> GetAllAsync()
        => await _vehicleImageRepository.GetAllAsync();

    public async Task AddAsync(VehicleImage vehicleImage)
    {
        await _vehicleImageRepository.AddAsync(vehicleImage);
        await _vehicleImageRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(VehicleImage vehicleImage)
    {
        _vehicleImageRepository.Update(vehicleImage);
        await _vehicleImageRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicleImage = await _vehicleImageRepository.GetByIdAsync(id);
        if (vehicleImage != null)
        {
            _vehicleImageRepository.RemoveHardDelete(vehicleImage);
            await _vehicleImageRepository.SaveChangesAsync();
        }
    }
} 