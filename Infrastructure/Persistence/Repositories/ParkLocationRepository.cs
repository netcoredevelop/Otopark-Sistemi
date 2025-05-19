using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class ParkLocationRepository : Repository<ParkLocation, int>, IParkLocationRepository
{
    public ParkLocationRepository(ApplicationDbContext context, ICurrentUserService currentUserService) 
        : base(context, currentUserService)
    {
    }
    
    // ParkLocation'a özgü ek metod implementasyonları buraya eklenebilir
} 