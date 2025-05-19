using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ParkLocationService : IParkLocationService
{
    private readonly IParkLocationRepository _parkLocationRepository;

    public ParkLocationService(IParkLocationRepository parkLocationRepository)
    {
        _parkLocationRepository = parkLocationRepository;
    }

    public async Task<ParkLocation?> GetByIdAsync(int id)
        => await _parkLocationRepository.GetByIdAsync(id);

    public async Task<IEnumerable<ParkLocation>> GetAllAsync()
        => await _parkLocationRepository.GetAllAsync();

    public async Task AddAsync(ParkLocation parkLocation)
    {
        await _parkLocationRepository.AddAsync(parkLocation);
        await _parkLocationRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(ParkLocation parkLocation)
    {
        _parkLocationRepository.Update(parkLocation);
        await _parkLocationRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var parkLocation = await _parkLocationRepository.GetByIdAsync(id);
        if (parkLocation != null)
        {
            _parkLocationRepository.Remove(parkLocation);
            await _parkLocationRepository.SaveChangesAsync();
        }
    }
} 