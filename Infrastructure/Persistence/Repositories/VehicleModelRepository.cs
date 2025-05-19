using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class VehicleModelRepository : Repository<VehicleModel, int>, IVehicleModelRepository
{
    public VehicleModelRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // VehicleModel'e özgü ek metod implementasyonları buraya eklenebilir
} 