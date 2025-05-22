using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITransactionCategoryRepository : IRepository<TransactionCategory, int>
{
    Task<IEnumerable<TransactionCategory>> GetActiveCategoriesAsync();
    Task<IEnumerable<TransactionCategory>> GetCategoriesByTypeAsync(TransactionType type);
} 