using Domain.Entities;

namespace Application.Interfaces;

public interface IBranchRepository : IRepository<Branch, int>
{
    // Branch'a özgü ek metodlar buraya eklenebilir
} 