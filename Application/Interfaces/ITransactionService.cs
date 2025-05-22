using Domain.Entities;

namespace Application.Interfaces;

public interface ITransactionService
{
    Task<Transaction?> GetByIdAsync(int id);
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<IEnumerable<Transaction>> GetTransactionsByBranchAsync(int branchId);
    Task<IEnumerable<Transaction>> GetTransactionsByVehicleAsync(int vehicleId);
    Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId);
    Task AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAsync(int id);
} 