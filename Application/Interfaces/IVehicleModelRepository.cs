using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleModelRepository : IRepository<VehicleModel, int>
{
    // VehicleModel'e özgü ek metodlar buraya eklenebilir
} 