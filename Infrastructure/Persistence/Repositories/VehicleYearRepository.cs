using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class VehicleYearRepository : Repository<VehicleYear, int>, IVehicleYearRepository
{
    public VehicleYearRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // VehicleYear'a özgü ek metod implementasyonları buraya eklenebilir
} 