using System.Linq.Expressions;
using Domain.Common;
using Domain.Entities;

namespace Application.Interfaces;

public interface IRepository<T, TId> where T : BaseEntity<TId>
{
    Task<T?> GetByIdAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate=null, params Expression<Func<T, object>>[] includes);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    void RemoveHardDelete(T entity);
    Task<int> SaveChangesAsync();
    
    // Pagination methods
    Task<PaginatedList<T>> GetPagedAsync(int pageIndex, int pageSize);
    Task<PaginatedList<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);
} 