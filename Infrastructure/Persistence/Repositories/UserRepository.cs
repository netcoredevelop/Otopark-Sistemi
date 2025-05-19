using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class UserRepository : Repository<User, int>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // User'a özgü ek metod implementasyonları buraya eklenebilir
} 