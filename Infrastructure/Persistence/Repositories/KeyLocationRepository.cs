using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class KeyLocationRepository : Repository<KeyLocation, int>, IKeyLocationRepository
{
    public KeyLocationRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // KeyLocation'a özgü ek metod implementasyonları buraya eklenebilir
} 