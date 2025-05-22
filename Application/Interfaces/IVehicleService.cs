using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces;

public interface IVehicleService
{
    Task<Vehicle?> GetByIdAsync(int id);
    Task<IEnumerable<Vehicle>> GetAllAsync();
    bool GetPlateNumber(string PlateNumber);
    Task AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    Task DeleteAsync(int id);
    Task HardDeleteAsync(int id);
    Task<IEnumerable<Vehicle>> GetDeletedVehiclesAsync();

    
    // Pagination methods
    Task<PaginatedList<Vehicle>> GetPagedAsync(int pageIndex, int pageSize);
    Task<PaginatedList<Vehicle>> GetPagedAsync(Expression<Func<Vehicle, bool>> predicate, int pageIndex, int pageSize, params Expression<Func<Vehicle, object>>[] includes);

    // Report method
    Task<IEnumerable<Vehicle>> GetVehiclesForReportAsync(
        DateTime? startDate = null,
        DateTime? endDate = null,
        int? branchId = null,
        int? vehicleTypeId = null,
        int? vehicleBrandId = null,
        int? vehicleModelId = null,
        int? vehicleColorId = null);
} 