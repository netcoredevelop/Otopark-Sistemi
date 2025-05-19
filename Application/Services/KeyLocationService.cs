using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class KeyLocationService : IKeyLocationService
{
    private readonly IKeyLocationRepository _keyLocationRepository;

    public KeyLocationService(IKeyLocationRepository keyLocationRepository)
    {
        _keyLocationRepository = keyLocationRepository;
    }

    public async Task<KeyLocation?> GetByIdAsync(int id)
        => await _keyLocationRepository.GetByIdAsync(id);

    public async Task<IEnumerable<KeyLocation>> GetAllAsync()
        => await _keyLocationRepository.GetAllAsync();

    public async Task AddAsync(KeyLocation keyLocation)
    {
        await _keyLocationRepository.AddAsync(keyLocation);
        await _keyLocationRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(KeyLocation keyLocation)
    {
        _keyLocationRepository.Update(keyLocation);
        await _keyLocationRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var keyLocation = await _keyLocationRepository.GetByIdAsync(id);
        if (keyLocation != null)
        {
            _keyLocationRepository.Remove(keyLocation);
            await _keyLocationRepository.SaveChangesAsync();
        }
    }
} 