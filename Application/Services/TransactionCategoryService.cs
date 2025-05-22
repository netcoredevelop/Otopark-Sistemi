using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TransactionCategoryService : ITransactionCategoryService
{
    private readonly ITransactionCategoryRepository _transactionCategoryRepository;

    public TransactionCategoryService(ITransactionCategoryRepository transactionCategoryRepository)
    {
        _transactionCategoryRepository = transactionCategoryRepository;
    }

    public async Task<TransactionCategory?> GetByIdAsync(int id)
        => await _transactionCategoryRepository.GetByIdAsync(id);

    public async Task<IEnumerable<TransactionCategory>> GetAllAsync()
        => await _transactionCategoryRepository.GetAllAsync();

    public async Task<IEnumerable<TransactionCategory>> GetActiveCategoriesAsync()
        => await _transactionCategoryRepository.GetActiveCategoriesAsync();

    public async Task<IEnumerable<TransactionCategory>> GetCategoriesByTypeAsync(TransactionType type)
        => await _transactionCategoryRepository.GetCategoriesByTypeAsync(type);

    public async Task AddAsync(TransactionCategory category)
    {
        await _transactionCategoryRepository.AddAsync(category);
        await _transactionCategoryRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(TransactionCategory category)
    {
        _transactionCategoryRepository.Update(category);
        await _transactionCategoryRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _transactionCategoryRepository.GetByIdAsync(id);
        if (category != null)
        {
            _transactionCategoryRepository.RemoveHardDelete(category);
            await _transactionCategoryRepository.SaveChangesAsync();
        }
    }
} 