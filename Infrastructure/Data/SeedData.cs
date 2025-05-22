using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Seeds
{
    public class SeedData
    {
        private readonly ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!await _context.Roles.AnyAsync())
            {
                await SeedRolesAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.Users.AnyAsync())
            {
                await SeedUsersAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.VehicleTypes.AnyAsync())
            {
                await SeedVehicleTypesAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.VehicleColors.AnyAsync())
            {
                await SeedVehicleColorsAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.VehicleBrands.AnyAsync())
            {
                await SeedVehicleBrandsAndModelsAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.Branches.AnyAsync())
            {
                await SeedBranchesAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.ParkLocations.AnyAsync())
            {
                await SeedParkLocationsAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.KeyLocations.AnyAsync())
            {
                await SeedKeyLocationsAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.LinkingReasons.AnyAsync())
            {
                await SeedLinkingReasonsAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.LinkingRegions.AnyAsync())
            {
                await SeedLinkingRegionsAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.EnforcementOffices.AnyAsync())
            {
                await SeedEnforcementOfficesAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.VehicleYears.AnyAsync())
            {
                await SeedVehicleYearsAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.Vehicles.AnyAsync())
            {
                await SeedVehiclesAsync();

                await _context.SaveChangesAsync();
            }

            if (!await _context.EnforcementRecords.AnyAsync())
            {
                await SeedEnforcementRecordsAsync();

                await _context.SaveChangesAsync();
            }


            if (!await _context.TransactionCategories.AnyAsync())
            {
                await SeedTransactionCategoriesAsync();

                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
        }

        private async Task SeedTransactionCategoriesAsync()
        {
            var transactionCategories = new List<TransactionCategory>()
            {
                new TransactionCategory
            {
                Id = 1,
                Name = "Otopark Gelirleri",
                Description = "Otopark gelirleri için kullanılan kategori",
                CreatedDate = DateTime.Now,
                CreatedBy = "System",
                Type = TransactionType.Gelir,
                IsActive = true
            },
                new TransactionCategory
            {
                Id = 2,
                Name = "Çekici Giderleri",
                Description = "Çekici giderleri için kullanılan kategori",
                CreatedDate = DateTime.Now,
                CreatedBy = "System",
                Type = TransactionType.Gider,
                IsActive = true
            }
            };
            await _context.TransactionCategories.AddRangeAsync(transactionCategories);
        }

        private async Task SeedRolesAsync()
        {
            var roles = new List<Role>
            {
                new Role { Name = "Admin" },
            };

            await _context.Roles.AddRangeAsync(roles);
        }

        private async Task SeedUsersAsync()
        {
            var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

            var users = new List<User>
            {
                new User
                {
                    Username = "admin",
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    UserRoles = new List<UserRole> { new UserRole { RoleId = adminRole.Id } }
                }
            };

            await _context.Users.AddRangeAsync(users);
        }

        private async Task SeedVehicleTypesAsync()
        {
            var vehicleTypes = new List<VehicleType>
            {
                new VehicleType { Name = "Otomobil" },
                new VehicleType { Name = "Minibüs" },
                new VehicleType { Name = "Otobüs" },
                new VehicleType { Name = "Kamyonet" },
                new VehicleType { Name = "Kamyon" },
                new VehicleType { Name = "Motosiklet" }
            };

            await _context.VehicleTypes.AddRangeAsync(vehicleTypes);
        }

        private async Task SeedVehicleColorsAsync()
        {
            var vehicleColors = new List<VehicleColor>
            {
                new VehicleColor { Name = "Siyah" },
                new VehicleColor { Name = "Beyaz" },
                new VehicleColor { Name = "Gri" },
                new VehicleColor { Name = "Kırmızı" },
                new VehicleColor { Name = "Mavi" },
                new VehicleColor { Name = "Yeşil" },
                new VehicleColor { Name = "Sarı" },
                new VehicleColor { Name = "Kahverengi" }
            };

            await _context.VehicleColors.AddRangeAsync(vehicleColors);
        }

        private async Task SeedVehicleBrandsAndModelsAsync()
        {
            // Toyota
            var toyota = new VehicleBrand { Name = "Toyota" };
            await _context.VehicleBrands.AddAsync(toyota);
            await _context.SaveChangesAsync();

            var toyotaModels = new List<VehicleModel>
            {
                new VehicleModel { Name = "Corolla", VehicleBrandId = toyota.Id },
                new VehicleModel { Name = "Camry", VehicleBrandId = toyota.Id },
                new VehicleModel { Name = "RAV4", VehicleBrandId = toyota.Id },
                new VehicleModel { Name = "Yaris", VehicleBrandId = toyota.Id }
            };

            await _context.VehicleModels.AddRangeAsync(toyotaModels);

            // Honda
            var honda = new VehicleBrand { Name = "Honda" };
            await _context.VehicleBrands.AddAsync(honda);
            await _context.SaveChangesAsync();

            var hondaModels = new List<VehicleModel>
            {
                new VehicleModel { Name = "Civic", VehicleBrandId = honda.Id },
                new VehicleModel { Name = "Accord", VehicleBrandId = honda.Id },
                new VehicleModel { Name = "CR-V", VehicleBrandId = honda.Id },
                new VehicleModel { Name = "Jazz", VehicleBrandId = honda.Id }
            };

            await _context.VehicleModels.AddRangeAsync(hondaModels);

            // Volkswagen
            var volkswagen = new VehicleBrand { Name = "Volkswagen" };
            await _context.VehicleBrands.AddAsync(volkswagen);
            await _context.SaveChangesAsync();

            var volkswagenModels = new List<VehicleModel>
            {
                new VehicleModel { Name = "Golf", VehicleBrandId = volkswagen.Id },
                new VehicleModel { Name = "Passat", VehicleBrandId = volkswagen.Id },
                new VehicleModel { Name = "Polo", VehicleBrandId = volkswagen.Id },
                new VehicleModel { Name = "Tiguan", VehicleBrandId = volkswagen.Id }
            };

            await _context.VehicleModels.AddRangeAsync(volkswagenModels);
        }

        private async Task SeedBranchesAsync()
        {
            var branches = new List<Branch>
            {
                new Branch { Name = "Merkez Şube", Capacity = 100 },
                new Branch { Name = "Kadıköy Şube", Capacity = 75 },
                new Branch { Name = "Beşiktaş Şube", Capacity = 50 },
                new Branch { Name = "Üsküdar Şube", Capacity = 60 },
                new Branch { Name = "Şişli Şube", Capacity = 80 }
            };

            await _context.Branches.AddRangeAsync(branches);
        }

        private async Task SeedParkLocationsAsync()
        {
            var parkLocations = new List<ParkLocation>
            {
                new ParkLocation { Name = "A Blok" },
                new ParkLocation { Name = "B Blok" },
                new ParkLocation { Name = "C Blok" },
                new ParkLocation { Name = "D Blok" },
                new ParkLocation { Name = "E Blok" }
            };

            await _context.ParkLocations.AddRangeAsync(parkLocations);
        }

        private async Task SeedKeyLocationsAsync()
        {
            var keyLocations = new List<KeyLocation>
            {
                new KeyLocation { Name = "Anahtar Dolabı 1" },
                new KeyLocation { Name = "Anahtar Dolabı 2" },
                new KeyLocation { Name = "Anahtar Dolabı 3" },
                new KeyLocation { Name = "Güvenlik Odası" },
                new KeyLocation { Name = "Resepsiyon" }
            };

            await _context.KeyLocations.AddRangeAsync(keyLocations);
        }

        private async Task SeedLinkingReasonsAsync()
        {
            var linkingReasons = new List<LinkingReason>
            {
                new LinkingReason { Name = "Trafik İhlali" },
                new LinkingReason { Name = "Park Yeri İhlali" },
                new LinkingReason { Name = "Hız İhlali" },
                new LinkingReason { Name = "Kırmızı Işık İhlali" },
                new LinkingReason { Name = "Emniyet Şeridi İhlali" }
            };

            await _context.LinkingReasons.AddRangeAsync(linkingReasons);
        }

        private async Task SeedLinkingRegionsAsync()
        {
            var linkingRegions = new List<LinkingRegion>
            {
                new LinkingRegion { Name = "Kadıköy" },
                new LinkingRegion { Name = "Beşiktaş" },
                new LinkingRegion { Name = "Üsküdar" },
                new LinkingRegion { Name = "Şişli" },
                new LinkingRegion { Name = "Beyoğlu" }
            };

            await _context.LinkingRegions.AddRangeAsync(linkingRegions);
        }

        private async Task SeedEnforcementOfficesAsync()
        {
            var enforcementOffices = new List<EnforcementOffice>
            {
                new EnforcementOffice { Name = "Kadıköy Zabıta Müdürlüğü" },
                new EnforcementOffice { Name = "Beşiktaş Zabıta Müdürlüğü" },
                new EnforcementOffice { Name = "Üsküdar Zabıta Müdürlüğü" },
                new EnforcementOffice { Name = "Şişli Zabıta Müdürlüğü" },
                new EnforcementOffice { Name = "Beyoğlu Zabıta Müdürlüğü" }
            };

            await _context.EnforcementOffices.AddRangeAsync(enforcementOffices);
        }

        private async Task SeedVehicleYearsAsync()
        {
            var currentYear = DateTime.Now.Year;
            var vehicleYears = new List<VehicleYear>();

            for (int year = currentYear - 30; year <= currentYear; year++)
            {
                vehicleYears.Add(new VehicleYear { Name = year.ToString() });
            }

            await _context.VehicleYears.AddRangeAsync(vehicleYears);
        }

        private async Task SeedVehiclesAsync()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    PlateNumber = "34ABC123",
                    VehicleOnwer = "Ahmet Yılmaz",
                    VehicleOnwerIdentityNumber = 12345678901,
                    BookSequenceNo = "B001",
                    LinkingTeamNumber = "T001",
                    EntryDate = DateTime.Now.AddDays(-5),
                    LinkingAdditionalInformation = "Test araç 1",
                    MuhammenBedeli = 5000,
                    Description = "Test açıklama 1",
                    BranchId = 1,
                    VehicleTypeId = 1,
                    VehicleBrandId = 1,
                    VehicleModelId = 1,
                    VehicleYearId = 1,
                    VehicleColorId = 1,
                    KeyLocationId = 1,
                    LinkingRegionId = 1,
                    LinkingReasonId = 1,
                    ParkLocationId = 1
                },
                new Vehicle
                {
                    PlateNumber = "34DEF456",
                    VehicleOnwer = "Mehmet Demir",
                    VehicleOnwerIdentityNumber = 98765432109,
                    BookSequenceNo = "B002",
                    LinkingTeamNumber = "T002",
                    EntryDate = DateTime.Now.AddDays(-3),
                    LinkingAdditionalInformation = "Test araç 2",
                    MuhammenBedeli = 7500,
                    Description = "Test açıklama 2",
                    BranchId = 2,
                    VehicleTypeId = 1,
                    VehicleBrandId = 2,
                    VehicleModelId = 5,
                    VehicleYearId = 2,
                    VehicleColorId = 2,
                    KeyLocationId = 2,
                    LinkingRegionId = 2,
                    LinkingReasonId = 2,
                    ParkLocationId = 2
                }
            };

            await _context.Vehicles.AddRangeAsync(vehicles);
        }

        private async Task SeedEnforcementRecordsAsync()
        {
            var enforcementRecords = new List<EnforcementRecord>
            {
                new EnforcementRecord
                {
                    RecordDate = DateTime.Now.AddDays(-5),
                    DossierNumber = "D001",
                    EnforcementOfficeId = 1,
                    VehicleId = 1,
                    CreatedAt = DateTime.Now.AddDays(-5)
                },
                new EnforcementRecord
                {
                    RecordDate = DateTime.Now.AddDays(-3),
                    DossierNumber = "D002",
                    EnforcementOfficeId = 2,
                    VehicleId = 2,
                    CreatedAt = DateTime.Now.AddDays(-3)
                }
            };

            await _context.EnforcementRecords.AddRangeAsync(enforcementRecords);
        }
    }
}