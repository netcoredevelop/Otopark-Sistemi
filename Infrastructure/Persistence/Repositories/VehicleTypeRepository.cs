using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class VehicleTypeRepository : Repository<VehicleType, int>, IVehicleTypeRepository
{
    public VehicleTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // VehicleType'a özgü ek metod implementasyonları buraya eklenebilir
} 