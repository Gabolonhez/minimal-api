using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace minimal_api.Migrations
{
    /// <inheritdoc />
    public partial class VehicleMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admnistrators",
                columns: new[] { "Id", "Email", "Password", "Profile" },
                values: new object[] { 1, "admnistrator@test.com", "123456", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admnistrators",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
