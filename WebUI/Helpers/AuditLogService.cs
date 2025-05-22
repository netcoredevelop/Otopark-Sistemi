using Domain.Common;
using Infrastructure.Persistence.Context;

public interface IAuditLogService
{
    Task<PaginatedList<AuditLog>> GetPagedAsync(int pageIndex, int pageSize, string? search = null);
    Task<AuditLog?> GetByIdAsync(int id);
    Task<AuditLog?> Delete(int id);


}

public class AuditLogService : IAuditLogService
{
    private readonly ApplicationDbContext _db;
    public AuditLogService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PaginatedList<AuditLog>> GetPagedAsync(int pageIndex, int pageSize, string? search = null)
    {
        var query = _db.AuditLogs.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(x =>
                x.UserName.Contains(search) ||
                x.OperationType.Contains(search) ||
                x.EntityName.Contains(search) ||
                x.EntityId.Contains(search) ||
                x.ActionName.Contains(search) ||
                x.ControllerName.Contains(search)
            );
        }

        query = query.OrderByDescending(x => x.OperationDate);

        return await PaginatedList<AuditLog>.CreateAsync(query, pageIndex, pageSize);
    }

    public async Task<AuditLog?> GetByIdAsync(int id)
    {
        return await _db.AuditLogs.FindAsync(id);
    }

    public Task<AuditLog?> Delete(int id)
    {

        var entity= _db.AuditLogs.FirstOrDefault(x=>x.Id==id);
        _db.AuditLogs.Remove(entity);
        _db.SaveChanges();
        return Task.FromResult<AuditLog?>(null);
    }
}