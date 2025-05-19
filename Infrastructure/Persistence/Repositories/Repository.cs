using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class Repository<T, TId> : IRepository<T, TId> where T : BaseEntity<TId>
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;
    protected readonly ICurrentUserService? _currentUserService;

    public Repository(ApplicationDbContext context, ICurrentUserService? currentUserService = null)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _currentUserService = currentUserService;
    }

    public virtual async Task<T?> GetByIdAsync(TId id)
    {
        return await _dbSet
            .Where(x => x.DeletedDate == null)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.Where(x => x.DeletedDate == null).ToListAsync();

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(x => x.DeletedDate == null).Where(predicate).ToListAsync();

    public async Task AddAsync(T entity)
    {
        entity.CreatedDate = DateTime.Now;
        entity.CreatedBy = _currentUserService?.UserName;
        await _dbSet.AddAsync(entity);
    }

    public virtual void Update(T entity)
    {
        entity.UpdatedDate = DateTime.Now;
        entity.UpdatedBy = _currentUserService?.UserName;
        _dbSet.Update(entity);
    }

    public virtual void Remove(T entity)
    {
        entity.DeletedDate = DateTime.Now;
        entity.UpdatedBy = _currentUserService?.UserName;
        _dbSet.Update(entity);
    }


    public virtual void RemoveHardDelete(T entity)
    {
        _dbSet.Remove(entity);
    }



    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    // Pagination implementations
    public virtual async Task<PaginatedList<T>> GetPagedAsync(int pageIndex, int pageSize)
    {
        var query = _dbSet.Where(x => x.DeletedDate == null);
        return await PaginatedList<T>.CreateAsync(query, pageIndex, pageSize);
    }

    public virtual async Task<PaginatedList<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
    {
        var query = _dbSet.Where(x => x.DeletedDate == null).Where(predicate);
        return await PaginatedList<T>.CreateAsync(query, pageIndex, pageSize);
    }

    // Silinen kayıtları getirmek için yeni metod
    public virtual async Task<IEnumerable<T>> GetDeletedAsync()
        => await _dbSet.Where(x => x.DeletedDate != null).ToListAsync();

    // Silinen bir kaydı geri getirmek için yeni metod
    public virtual async Task RestoreAsync(TId id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            entity.DeletedDate = null;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = _currentUserService?.UserName;
            _dbSet.Update(entity);
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();  // Start with the full query set

        // If a predicate is provided, apply it
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // Apply includes if provided
        if (includes != null && includes.Any())
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.ToListAsync();
    }
} 