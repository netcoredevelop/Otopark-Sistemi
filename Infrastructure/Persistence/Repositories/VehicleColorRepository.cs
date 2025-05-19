using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class VehicleColorRepository : Repository<VehicleColor, int>, IVehicleColorRepository
{
    public VehicleColorRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // VehicleColor'a özgü ek metod implementasyonları buraya eklenebilir
} 