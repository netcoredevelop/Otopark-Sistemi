using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IVehicleRepository : IRepository<Vehicle, int>
{
    IQueryable<Vehicle> GetQueryable();
    bool? GetByPlateNumberAsync(string plateNumber);
    Task<IEnumerable<Vehicle>> GetDeletedVehiclesAsync();
    Task<PaginatedList<Vehicle>> GetPagedAsync(int pageIndex, int pageSize);
    Task<PaginatedList<Vehicle>> GetPagedAsync(Expression<Func<Vehicle, bool>> predicate, int pageIndex, int pageSize);
    Task<PaginatedList<Vehicle>> GetPagedAsync(Expression<Func<Vehicle, bool>> predicate, int pageIndex, int pageSize, params Expression<Func<Vehicle, object>>[] includes);

    // Vehicle'a özgü ek metodlar buraya eklenebilir
    // Örneğin:
    // Task<IEnumerable<Vehicle>> GetVehiclesByBrandAsync(int brandId);
} 