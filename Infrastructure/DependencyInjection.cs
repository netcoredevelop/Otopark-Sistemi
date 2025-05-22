using Application.Interfaces;
using Application.Services;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext ekleyin
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Repository'leri ekleyin
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IVehicleBrandRepository, VehicleBrandRepository>();
        services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
        services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
        services.AddScoped<IVehicleColorRepository, VehicleColorRepository>();
        services.AddScoped<IVehicleYearRepository, VehicleYearRepository>();
        services.AddScoped<IKeyLocationRepository, KeyLocationRepository>();
        services.AddScoped<ILinkingReasonRepository, LinkingReasonRepository>();
        services.AddScoped<ILinkingRegionRepository, LinkingRegionRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IParkLocationRepository, ParkLocationRepository>();
        services.AddScoped<IVehicleImageRepository, VehicleImageRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IEnforcementRecordRepository, EnforcementRecordRepository>();
        services.AddScoped<IEnforcementOfficeRepository, EnforcementOfficeRepository>();
        services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        
        // Servisleri ekleyin
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IVehicleBrandService, VehicleBrandService>();
        services.AddScoped<IVehicleModelService, VehicleModelService>();
        services.AddScoped<IVehicleTypeService, VehicleTypeService>();
        services.AddScoped<IVehicleColorService, VehicleColorService>();
        services.AddScoped<IVehicleYearService, VehicleYearService>();
        services.AddScoped<IKeyLocationService, KeyLocationService>();
        services.AddScoped<ILinkingReasonService, LinkingReasonService>();
        services.AddScoped<ILinkingRegionService, LinkingRegionService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IParkLocationService, ParkLocationService>();
        services.AddScoped<IVehicleImageService, VehicleImageService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IEnforcementRecordService, EnforcementRecordService>();
        services.AddScoped<IEnforcementOfficeService, EnforcementOfficeService>();
        services.AddScoped<ITransactionCategoryService, TransactionCategoryService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IRoleSeederService, RoleSeederService>();
        services.AddScoped<IPasswordService, PasswordService>();

        return services;
    }
} 