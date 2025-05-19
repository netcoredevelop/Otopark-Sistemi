using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VehicleYears",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleYears",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "VehicleYears",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "VehicleYears",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "VehicleYears",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VehicleTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "VehicleTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "VehicleTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "VehicleTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleModels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "VehicleModels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "VehicleModels",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VehicleImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleImages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "VehicleImages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "VehicleImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "VehicleImages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VehicleColors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleColors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "VehicleColors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "VehicleColors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "VehicleColors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VehicleBrands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleBrands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "VehicleBrands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "VehicleBrands",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "VehicleBrands",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "UserRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "UserRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "UserRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ParkLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ParkLocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ParkLocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ParkLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ParkLocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LinkingRegions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LinkingRegions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "LinkingRegions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "LinkingRegions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "LinkingRegions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LinkingReasons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LinkingReasons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "LinkingReasons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "LinkingReasons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "LinkingReasons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "KeyLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "KeyLocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "KeyLocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "KeyLocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "KeyLocations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EnforcementRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "EnforcementRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "EnforcementRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "EnforcementRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "EnforcementRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EnforcementOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "EnforcementOffices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "EnforcementOffices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "EnforcementOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "EnforcementOffices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Documents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Branches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Branches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Branches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Branches",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VehicleYears");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VehicleYears");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "VehicleYears");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VehicleYears");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "VehicleYears");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "VehicleTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VehicleImages");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VehicleImages");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "VehicleImages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VehicleImages");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "VehicleImages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VehicleColors");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VehicleColors");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "VehicleColors");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VehicleColors");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "VehicleColors");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VehicleBrands");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "VehicleBrands");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "VehicleBrands");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "VehicleBrands");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "VehicleBrands");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ParkLocations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ParkLocations");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ParkLocations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ParkLocations");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ParkLocations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LinkingRegions");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LinkingRegions");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "LinkingRegions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LinkingRegions");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "LinkingRegions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LinkingReasons");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LinkingReasons");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "LinkingReasons");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LinkingReasons");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "LinkingReasons");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "KeyLocations");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "KeyLocations");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "KeyLocations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "KeyLocations");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "KeyLocations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EnforcementRecords");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EnforcementRecords");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "EnforcementRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EnforcementRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "EnforcementRecords");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EnforcementOffices");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "EnforcementOffices");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "EnforcementOffices");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EnforcementOffices");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "EnforcementOffices");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Branches");
        }
    }
}
