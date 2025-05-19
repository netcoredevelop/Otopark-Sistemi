using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleBrand> VehicleBrands => Set<VehicleBrand>();
    public DbSet<VehicleModel> VehicleModels => Set<VehicleModel>();
    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
    public DbSet<VehicleColor> VehicleColors => Set<VehicleColor>();
    public DbSet<VehicleYear> VehicleYears => Set<VehicleYear>();
    public DbSet<KeyLocation> KeyLocations => Set<KeyLocation>();
    public DbSet<LinkingReason> LinkingReasons => Set<LinkingReason>();
    public DbSet<LinkingRegion> LinkingRegions => Set<LinkingRegion>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<ParkLocation> ParkLocations => Set<ParkLocation>();
    public DbSet<VehicleImage> VehicleImages => Set<VehicleImage>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<EnforcementRecord> EnforcementRecords => Set<EnforcementRecord>();
    public DbSet<EnforcementOffice> EnforcementOffices => Set<EnforcementOffice>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}