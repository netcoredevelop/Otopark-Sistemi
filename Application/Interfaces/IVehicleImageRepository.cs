using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleImageRepository : IRepository<VehicleImage, int>
{
    // VehicleImage'a özgü ek metodlar buraya eklenebilir
} 