using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRoleRepository : IRepository<UserRole, int>
{
    // UserRole'a özgü ek metodlar buraya eklenebilir
} 