using Domain.Entities;

namespace Application.Interfaces;

public interface IBranchService
{
    Task<Branch?> GetByIdAsync(int id);
    Task<IEnumerable<Branch>> GetAllAsync();
    Task AddAsync(Branch branch);
    Task UpdateAsync(Branch branch);
    Task DeleteAsync(int id);
} 