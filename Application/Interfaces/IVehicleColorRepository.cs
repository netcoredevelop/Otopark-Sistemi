using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleColorRepository : IRepository<VehicleColor, int>
{
    // VehicleColor'a özgü ek metodlar buraya eklenebilir
} 