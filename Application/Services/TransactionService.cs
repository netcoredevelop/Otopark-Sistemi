using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Transaction?> GetByIdAsync(int id)
        => await _transactionRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Transaction>> GetAllAsync()
        => await _transactionRepository.GetAllAsync();

    public async Task<IEnumerable<Transaction>> GetTransactionsByBranchAsync(int branchId)
        => await _transactionRepository.GetTransactionsByBranchAsync(branchId);

    public async Task<IEnumerable<Transaction>> GetTransactionsByVehicleAsync(int vehicleId)
        => await _transactionRepository.GetTransactionsByVehicleAsync(vehicleId);

    public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        => await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);

    public async Task<IEnumerable<Transaction>> GetTransactionsByCategoryAsync(int categoryId)
        => await _transactionRepository.GetTransactionsByCategoryAsync(categoryId);

    public async Task AddAsync(Transaction transaction)
    {
        await _transactionRepository.AddAsync(transaction);
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Transaction transaction)
    {
        _transactionRepository.Update(transaction);
        await _transactionRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction != null)
        {
            _transactionRepository.RemoveHardDelete(transaction);
            await _transactionRepository.SaveChangesAsync();
        }
    }
} 