using Domain.Entities;

namespace Application.Interfaces;

public interface IRoleRepository : IRepository<Role, int>
{
    // Role'a özgü ek metodlar buraya eklenebilir
} 