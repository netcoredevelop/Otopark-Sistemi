using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Persistence.Repositories;

public class BranchRepository : Repository<Branch, int>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    // Branch'a özgü ek metod implementasyonları buraya eklenebilir
} 