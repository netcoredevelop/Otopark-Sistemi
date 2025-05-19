using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository : IRepository<User, int>
{
    // User'a özgü ek metodlar buraya eklenebilir
} 