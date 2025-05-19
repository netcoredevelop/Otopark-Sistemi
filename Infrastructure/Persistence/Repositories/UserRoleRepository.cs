using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class UserRoleRepository : Repository<UserRole, int>, IUserRoleRepository
{
    public UserRoleRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // UserRole'a özgü ek metod implementasyonları buraya eklenebilir
} 