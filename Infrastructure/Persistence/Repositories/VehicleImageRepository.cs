using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class VehicleImageRepository : Repository<VehicleImage, int>, IVehicleImageRepository
{
    public VehicleImageRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // VehicleImage'a özgü ek metod implementasyonları buraya eklenebilir
} 