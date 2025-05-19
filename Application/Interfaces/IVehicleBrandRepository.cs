using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleBrandRepository : IRepository<VehicleBrand, int>
{
    // VehicleBrand'e özgü ek metodlar buraya eklenebilir
} 