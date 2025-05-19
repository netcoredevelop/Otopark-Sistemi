using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class EnforcementOfficeService : IEnforcementOfficeService
{
    private readonly IEnforcementOfficeRepository _enforcementOfficeRepository;

    public EnforcementOfficeService(IEnforcementOfficeRepository enforcementOfficeRepository)
    {
        _enforcementOfficeRepository = enforcementOfficeRepository;
    }

    public async Task<EnforcementOffice?> GetByIdAsync(int id)
        => await _enforcementOfficeRepository.GetByIdAsync(id);

    public async Task<IEnumerable<EnforcementOffice>> GetAllAsync()
        => await _enforcementOfficeRepository.GetAllAsync();

    public async Task AddAsync(EnforcementOffice enforcementOffice)
    {
        await _enforcementOfficeRepository.AddAsync(enforcementOffice);
        await _enforcementOfficeRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(EnforcementOffice enforcementOffice)
    {
        _enforcementOfficeRepository.Update(enforcementOffice);
        await _enforcementOfficeRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var enforcementOffice = await _enforcementOfficeRepository.GetByIdAsync(id);
        if (enforcementOffice != null)
        {
            _enforcementOfficeRepository.Remove(enforcementOffice);
            await _enforcementOfficeRepository.SaveChangesAsync();
        }
    }
} 