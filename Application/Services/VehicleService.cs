using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
        => await _vehicleRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
        => await _vehicleRepository.GetAllAsync();

    public async Task AddAsync(Vehicle vehicle)
    {
        await _vehicleRepository.AddAsync(vehicle);
        await _vehicleRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _vehicleRepository.Update(vehicle);
        await _vehicleRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle != null)
        {
            _vehicleRepository.Remove(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }
    }
    public async Task HardDeleteAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle != null)
        {
            _vehicleRepository.RemoveHardDelete(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Vehicle>> GetDeletedVehiclesAsync()
        => await _vehicleRepository.GetDeletedVehiclesAsync();

    // Pagination implementations
    public async Task<PaginatedList<Vehicle>> GetPagedAsync(int pageIndex, int pageSize)
    {
        return await _vehicleRepository.GetPagedAsync(pageIndex, pageSize);
    }

    public async Task<PaginatedList<Vehicle>> GetPagedAsync(Expression<Func<Vehicle, bool>> predicate, int pageIndex, int pageSize, params Expression<Func<Vehicle, object>>[] includes)
    {
        return await _vehicleRepository.GetPagedAsync(predicate, pageIndex, pageSize, includes);
    }

    public async Task<IEnumerable<Vehicle>> GetVehiclesForReportAsync(
        DateTime? startDate = null,
        DateTime? endDate = null,
        int? branchId = null,
        int? vehicleTypeId = null,
        int? vehicleBrandId = null,
        int? vehicleModelId = null,
        int? vehicleColorId = null)
    {
        var query = await _vehicleRepository.GetAllAsync(
            predicate: x=>x.DeletedDate==null,  // Filtreleme yapýlmadýðý için null
            includes: new Expression<Func<Vehicle, object>>[]  // `includes` parametresi burada doðru bir þekilde geçildi
            {
        v => v.Branch,
        v => v.VehicleType,
        v => v.VehicleBrand,
        v => v.VehicleModel,
        v => v.VehicleYear,
        v => v.VehicleColor,
        v => v.KeyLocation,
        v => v.LinkingRegion,
        v => v.LinkingReason,
        v => v.ParkLocation
            }
        );

        if (startDate.HasValue)
            query = query.Where(v => v.EntryDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(v => v.EntryDate <= endDate.Value);

        if (branchId.HasValue)
            query = query.Where(v => v.BranchId == branchId.Value);

        if (vehicleTypeId.HasValue)
            query = query.Where(v => v.VehicleTypeId == vehicleTypeId.Value);

        if (vehicleBrandId.HasValue)
            query = query.Where(v => v.VehicleBrandId == vehicleBrandId.Value);

        if (vehicleModelId.HasValue)
            query = query.Where(v => v.VehicleModelId == vehicleModelId.Value);

        if (vehicleColorId.HasValue)
            query = query.Where(v => v.VehicleColorId == vehicleColorId.Value);

        return query;
    }
}