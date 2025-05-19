using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class EnforcementRecordService : IEnforcementRecordService
{
    private readonly IEnforcementRecordRepository _enforcementRecordRepository;

    public EnforcementRecordService(IEnforcementRecordRepository enforcementRecordRepository)
    {
        _enforcementRecordRepository = enforcementRecordRepository;
    }

    public async Task<EnforcementRecord?> GetByIdAsync(int id)
        => await _enforcementRecordRepository.GetByIdAsync(id);

    public async Task<IEnumerable<EnforcementRecord>> GetAllAsync()
        => await _enforcementRecordRepository.GetAllAsync();

    public async Task AddAsync(EnforcementRecord enforcementRecord)
    {
        await _enforcementRecordRepository.AddAsync(enforcementRecord);
        await _enforcementRecordRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(EnforcementRecord enforcementRecord)
    {
        _enforcementRecordRepository.Update(enforcementRecord);
        await _enforcementRecordRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var enforcementRecord = await _enforcementRecordRepository.GetByIdAsync(id);
        if (enforcementRecord != null)
        {
            _enforcementRecordRepository.RemoveHardDelete(enforcementRecord);
            await _enforcementRecordRepository.SaveChangesAsync();
        }
    }
} 