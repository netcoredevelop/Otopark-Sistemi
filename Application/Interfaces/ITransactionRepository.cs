using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITransactionRepository : IRepository<Transaction, int>
{
    Task<IEnumerable<Transaction>> GetTransactionsByBranchAsync(int branchId);
    Task<IEnumerable<Transaction>> GetTransactionsByVehicleAsync(int vehicleId);
    Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId);
} 