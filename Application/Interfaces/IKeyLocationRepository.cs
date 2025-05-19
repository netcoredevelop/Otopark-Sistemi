using Domain.Entities;

namespace Application.Interfaces;

public interface IKeyLocationRepository : IRepository<KeyLocation, int>
{
    // KeyLocation'a özgü ek metodlar buraya eklenebilir
} 