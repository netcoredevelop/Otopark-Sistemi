using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class RoleRepository : Repository<Role, int>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // Role'a özgü ek metod implementasyonları buraya eklenebilir
} 