using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleTypeRepository : IRepository<VehicleType, int>
{
    // VehicleType'a özgü ek metodlar buraya eklenebilir
} 