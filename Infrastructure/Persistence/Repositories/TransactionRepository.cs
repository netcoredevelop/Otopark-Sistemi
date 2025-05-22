using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class TransactionRepository : Repository<Transaction, int>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context, ICurrentUserService currentUserService)
        : base(context, currentUserService)
    {
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByBranchAsync(int branchId)
    {
        return await _dbSet
            .Where(t => t.BranchId == branchId && t.DeletedDate == null)
            .Include(t => t.TransactionCategory)
            .Include(t => t.Branch)
            .Include(t => t.Vehicle)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByVehicleAsync(int vehicleId)
    {
        return await _dbSet
            .Where(t => t.VehicleId == vehicleId && t.DeletedDate == null)
            .Include(t => t.TransactionCategory)
            .Include(t => t.Branch)
            .Include(t => t.Vehicle)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Where(t => t.TransactionDate >= startDate && t.TransactionDate <= endDate && t.DeletedDate == null)
            .Include(t => t.TransactionCategory)
            .Include(t => t.Branch)
            .Include(t => t.Vehicle)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId)
    {
        return await _dbSet
            .Where(t => t.TransactionCategoryId == categoryId && t.DeletedDate == null)
            .Include(t => t.TransactionCategory)
            .Include(t => t.Branch)
            .Include(t => t.Vehicle)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public override async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(t => t.TransactionCategory)
            .Include(t => t.Branch)
            .Include(t => t.Vehicle)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
} 