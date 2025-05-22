using Domain.Entities;

namespace Application.Interfaces;

public interface ITransactionCategoryService
{
    Task<TransactionCategory?> GetByIdAsync(int id);
    Task<IEnumerable<TransactionCategory>> GetAllAsync();
    Task<IEnumerable<TransactionCategory>> GetActiveCategoriesAsync();
    Task<IEnumerable<TransactionCategory>> GetCategoriesByTypeAsync(TransactionType type);
    Task AddAsync(TransactionCategory category);
    Task UpdateAsync(TransactionCategory category);
    Task DeleteAsync(int id);
} 