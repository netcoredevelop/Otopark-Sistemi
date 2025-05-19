using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class EnforcementOfficeRepository : Repository<EnforcementOffice, int>, IEnforcementOfficeRepository
{
    public EnforcementOfficeRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // EnforcementOffice'a özgü ek metod implementasyonları buraya eklenebilir
} 