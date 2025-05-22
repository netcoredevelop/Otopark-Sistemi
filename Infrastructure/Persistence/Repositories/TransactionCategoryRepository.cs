using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class TransactionCategoryRepository : Repository<TransactionCategory, int>, ITransactionCategoryRepository
{
    public TransactionCategoryRepository(ApplicationDbContext context, ICurrentUserService currentUserService)
        : base(context, currentUserService)
    {
    }

    public async Task<IEnumerable<TransactionCategory>> GetActiveCategoriesAsync()
    {
        return await _dbSet
            .Where(tc => tc.IsActive && tc.DeletedDate == null)
            .OrderBy(tc => tc.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<TransactionCategory>> GetCategoriesByTypeAsync(TransactionType type)
    {
        return await _dbSet
            .Where(tc => tc.Type == type && tc.IsActive && tc.DeletedDate == null)
            .OrderBy(tc => tc.Name)
            .ToListAsync();
    }
} 