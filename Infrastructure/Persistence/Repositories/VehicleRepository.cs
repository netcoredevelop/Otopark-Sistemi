using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class VehicleRepository : Repository<Vehicle, int>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext context, ICurrentUserService currentUserService)
        : base(context, currentUserService)
    {
    }

    public IQueryable<Vehicle> GetQueryable()
    {
        return _dbSet
            .Where(v => v.DeletedDate == null)
            .Include(v => v.Branch)
            .Include(v => v.VehicleType)
            .Include(v => v.VehicleBrand)
            .Include(v => v.VehicleModel)
            .Include(v => v.VehicleYear)
            .Include(v => v.VehicleColor)
            .Include(v => v.KeyLocation)
            .Include(v => v.LinkingRegion)
            .Include(v => v.LinkingReason)
            .Include(v => v.ParkLocation);
    }

    public override async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(v => v.Branch)
            .Include(v => v.VehicleType)
            .Include(v => v.VehicleBrand)
            .Include(v => v.VehicleModel)
            .Include(v => v.VehicleYear)
            .Include(v => v.VehicleColor)
            .Include(v => v.KeyLocation)
            .Include(v => v.LinkingRegion)
            .Include(v => v.LinkingReason)
            .Include(v => v.Transactions)
            .Include(v => v.ParkLocation)
            .Include(v => v.EnforcementRecords)
                .ThenInclude(er => er.EnforcementOffice)
            .Include(v => v.Images)
            .Include(v => v.Documents)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public override async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _dbSet
            .Where(v => v.DeletedDate == null)
            .Include(v => v.Branch)
            .Include(v => v.VehicleType)
            .Include(v => v.VehicleBrand)
            .Include(v => v.VehicleModel)
            .Include(v => v.VehicleYear)
            .Include(v => v.VehicleColor)
            .Include(v => v.KeyLocation)
            .Include(v => v.LinkingRegion)
            .Include(v => v.LinkingReason)
            .Include(v => v.ParkLocation)
            .ToListAsync();
    }

    public async Task<PaginatedList<Vehicle>> GetPagedAsync(int pageIndex, int pageSize)
    {
        var query = GetQueryable();
        return await PaginatedList<Vehicle>.CreateAsync(query, pageIndex, pageSize);
    }

    public async Task<PaginatedList<Vehicle>> GetPagedAsync(Expression<Func<Vehicle, bool>> predicate, int pageIndex, int pageSize)
    {
        var query = GetQueryable().Where(predicate);
        return await PaginatedList<Vehicle>.CreateAsync(query, pageIndex, pageSize);
    }

    public async Task<PaginatedList<Vehicle>> GetPagedAsync(Expression<Func<Vehicle, bool>> predicate, int pageIndex, int pageSize, params Expression<Func<Vehicle, object>>[] includes)
    {
        var query = GetQueryable();

        // Apply predicate
        query = query.Where(predicate);

        // Apply includes
        if (includes != null && includes.Any())
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await PaginatedList<Vehicle>.CreateAsync(query, pageIndex, pageSize);
    }

    public override void Update(Vehicle entity)
    {
        var existingEntity = _dbSet
            .Include(v => v.Branch)
            .Include(v => v.VehicleType)
            .Include(v => v.VehicleBrand)
            .Include(v => v.VehicleModel)
            .Include(v => v.VehicleYear)
            .Include(v => v.VehicleColor)
            .Include(v => v.KeyLocation)
            .Include(v => v.LinkingRegion)
            .Include(v => v.LinkingReason)
            .Include(v => v.ParkLocation)
            .Include(v => v.EnforcementRecords)
                .ThenInclude(er => er.EnforcementOffice)
            .Include(v => v.Images)
            .Include(v => v.Documents)
            .FirstOrDefault(v => v.Id == entity.Id);

        if (existingEntity != null)
        {
            // Update scalar properties
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            // Update foreign keys
            existingEntity.BranchId = entity.BranchId;
            existingEntity.VehicleTypeId = entity.VehicleTypeId;
            existingEntity.VehicleBrandId = entity.VehicleBrandId;
            existingEntity.VehicleModelId = entity.VehicleModelId;
            existingEntity.VehicleYearId = entity.VehicleYearId;
            existingEntity.VehicleColorId = entity.VehicleColorId;
            existingEntity.KeyLocationId = entity.KeyLocationId;
            existingEntity.LinkingRegionId = entity.LinkingRegionId;
            existingEntity.LinkingReasonId = entity.LinkingReasonId;
            existingEntity.ParkLocationId = entity.ParkLocationId;

            // Update audit fields
            existingEntity.UpdatedDate = DateTime.Now;
            existingEntity.UpdatedBy = _currentUserService?.UserName;

            _dbSet.Update(existingEntity);
        }
    }

    public override void Remove(Vehicle entity)
    {
        var existingEntity = _dbSet
            .Include(v => v.Branch)
            .Include(v => v.VehicleType)
            .Include(v => v.VehicleBrand)
            .Include(v => v.VehicleModel)
            .Include(v => v.VehicleYear)
            .Include(v => v.VehicleColor)
            .Include(v => v.KeyLocation)
            .Include(v => v.LinkingRegion)
            .Include(v => v.LinkingReason)
            .Include(v => v.ParkLocation)
            .Include(v => v.EnforcementRecords)
                .ThenInclude(er => er.EnforcementOffice)
            .Include(v => v.Images)
            .Include(v => v.Documents)
            .FirstOrDefault(v => v.Id == entity.Id);

        if (existingEntity != null)
        {
            existingEntity.DeletedDate = DateTime.Now;
            existingEntity.UpdatedBy = _currentUserService?.UserName;
            _dbSet.Update(existingEntity);
        }
    }

    public async Task<IEnumerable<Vehicle>> GetDeletedVehiclesAsync()
    {
        return await _dbSet
            .Where(v => v.DeletedDate != null)
            .Include(v => v.Branch)
            .Include(v => v.VehicleType)
            .Include(v => v.VehicleBrand)
            .Include(v => v.VehicleModel)
            .Include(v => v.VehicleYear)
            .Include(v => v.VehicleColor)
            .Include(v => v.KeyLocation)
            .Include(v => v.LinkingRegion)
            .Include(v => v.LinkingReason)
            .Include(v => v.ParkLocation)
            .ToListAsync();
    }

    public bool? GetByPlateNumberAsync(string plateNumber)
    {
        var boolData = _dbSet.Any(x => x.PlateNumber == plateNumber && x.ExitDate==null);
        return boolData;
    }

    // Vehicle'a özgü ek metod implementasyonları buraya eklenebilir
    // Örneğin:
    // public async Task<IEnumerable<Vehicle>> GetVehiclesByBrandAsync(int brandId)
    // {
    //     return await FindAsync(v => v.VehicleBrandId == brandId);
    // }
}