using Microsoft.EntityFrameworkCore.Migrations;

namespace Sat.Recruitment.Repository.EF.Migrations
{
    public partial class addingdataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Money", "Name", "Phone", "UserType" },
                values: new object[] { 1, "Peru 2464", "Juan @marmol.com", 1234m, "Juan", "+5491154762312", "Normal" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Money", "Name", "Phone", "UserType" },
                values: new object[] { 2, "Alvear y Colombres", "Franco.Perez@gmail.com", 112234m, "Franco", "+534645213542", "Premium" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "Money", "Name", "Phone", "UserType" },
                values: new object[] { 3, "Garay y Otra Calle", "Agustina@gmail.com", 112234m, "Agustina", "+534645213542", "SuperUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
