using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inıt_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "System", new DateTime(2025, 5, 19, 12, 0, 0, 0, DateTimeKind.Utc), null, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { 1, "System", new DateTime(2025, 5, 19, 12, 0, 0, 0, DateTimeKind.Utc), null, "admin@otopark.com", "Admin", "User", "$2a$12$ABC123examplehashhashedvalueforbcryptXYZ", null, null, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "RoleId", "UpdatedBy", "UpdatedDate", "UserId" },
                values: new object[] { 1, "System", new DateTime(2025, 5, 19, 12, 0, 0, 0, DateTimeKind.Utc), null, 1, null, null, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
