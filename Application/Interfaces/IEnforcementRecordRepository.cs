using Domain.Entities;

namespace Application.Interfaces;

public interface IEnforcementRecordRepository : IRepository<EnforcementRecord, int>
{
    // EnforcementRecord'a özgü ek metodlar buraya eklenebilir
} 