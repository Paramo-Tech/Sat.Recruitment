using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sat.Recruitment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    phone = table.Column<string>(type: "TEXT", nullable: false),
                    userType = table.Column<string>(type: "TEXT", nullable: false),
                    money = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "address", "email", "money", "name", "phone", "userType" },
                values: new object[,]
                {
                    { 1, "+5491154762312", "Juan@marmol.com", 1382.08m, "Juan", "Peru 2464", "Normal" },
                    { 2, "+534645213542", "Franco.Perez@gmail.com", 336702m, "Franco", "Alvear y Colombres", "Premium" },
                    { 3, "+534641213542", "Agustina@gmail.com", 134680.8m, "Agustina", "Garay y Otra Calle", "SuperUser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
