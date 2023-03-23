using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sat.Recruitment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "address", "phone" },
                values: new object[] { "Peru 2464", "+5491154762312" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "address", "phone" },
                values: new object[] { "Alvear y Colombres", "+534645213542" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "address", "phone" },
                values: new object[] { "Garay y Otra Calle", "+534641213542" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "address", "phone" },
                values: new object[] { "+5491154762312", "Peru 2464" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "address", "phone" },
                values: new object[] { "+534645213542", "Alvear y Colombres" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "address", "phone" },
                values: new object[] { "+534641213542", "Garay y Otra Calle" });
        }
    }
}
