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
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<TransactionCategory> TransactionCategories => Set<TransactionCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        //// Sabit tarih
        //var fixedDate = new DateTime(2025, 5, 19, 12, 0, 0, DateTimeKind.Utc);

        //// Önceden hesaplanmış password hash (örnek)
        //// "Mehmet5255" şifresi için bcrypt hash (12 salt rounds)
        //var passwordHash = "$2a$12$eQ65yPg2F7rBe.yHmUgrDuqdf/xgPI3FSuX9vuc9jA4YaXytq8F7S";

        //modelBuilder.Entity<Role>().HasData(
        //    new Role
        //    {
        //        Id = 1,
        //        Name = "Admin",
        //        CreatedDate = fixedDate,
        //        CreatedBy = "System"
        //    }
        //);

        //modelBuilder.Entity<User>().HasData(
        //    new User
        //    {
        //        Id = 1,
        //        Username = "admin",
        //        FirstName = "Admin",
        //        LastName = "User",
        //        Email = "admin@otopark.com",
        //        PasswordHash = passwordHash,
        //        CreatedDate = fixedDate,
        //        CreatedBy = "System"
        //    }
        //);

        //modelBuilder.Entity<UserRole>().HasData(
        //    new UserRole
        //    {
        //        Id = 1,
        //        UserId = 1,
        //        RoleId = 1,
        //        CreatedDate = fixedDate,
        //        CreatedBy = "System"
        //    }
        //);

        base.OnModelCreating(modelBuilder);
    }
}