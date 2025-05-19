using Domain.Entities;

namespace Application.Interfaces;

public interface IParkLocationRepository : IRepository<ParkLocation, int>
{
    // ParkLocation'a özgü ek metodlar buraya eklenebilir
} 