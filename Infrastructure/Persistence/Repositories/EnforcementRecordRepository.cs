using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class EnforcementRecordRepository : Repository<EnforcementRecord, int>, IEnforcementRecordRepository
{
    public EnforcementRecordRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public override async Task<EnforcementRecord?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(er => er.EnforcementOffice)
            .Include(er => er.Vehicle)
            .FirstOrDefaultAsync(er => er.Id == id);
    }
    
    // EnforcementRecord'a özgü ek metod implementasyonları buraya eklenebilir
} 