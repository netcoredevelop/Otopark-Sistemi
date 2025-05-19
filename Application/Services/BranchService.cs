using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class BranchService : IBranchService
{
    private readonly IBranchRepository _branchRepository;

    public BranchService(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Branch?> GetByIdAsync(int id)
        => await _branchRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Branch>> GetAllAsync()
        => await _branchRepository.GetAllAsync();

    public async Task AddAsync(Branch branch)
    {
        await _branchRepository.AddAsync(branch);
        await _branchRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Branch branch)
    {
        _branchRepository.Update(branch);
        await _branchRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var branch = await _branchRepository.GetByIdAsync(id);
        if (branch != null)
        {
            _branchRepository.Remove(branch);
            await _branchRepository.SaveChangesAsync();
        }
    }
} 