using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class VehicleBrandRepository : Repository<VehicleBrand, int>, IVehicleBrandRepository
{
    public VehicleBrandRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // VehicleBrand'e özgü ek metod implementasyonları buraya eklenebilir
} 