using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "AuditLogs",
                newName: "OldData");

            migrationBuilder.AddColumn<string>(
                name: "NewData",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewData",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "OldData",
                table: "AuditLogs",
                newName: "Data");
        }
    }
}
