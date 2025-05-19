using Domain.Entities;

namespace Application.Interfaces;

public interface IVehicleYearRepository : IRepository<VehicleYear, int>
{
    // VehicleYear'a özgü ek metodlar buraya eklenebilir
} 