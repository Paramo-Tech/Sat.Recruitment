using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sat.Recruitment.Api.Migrations
{
    public partial class AddModelsToSqlDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    UserTypeId = table.Column<Guid>(nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserType",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");

            migrationBuilder.InsertData(
            table: "UserType",
            columns: new[] { "UserTypeId", "Name" },
            values: new object[] { Guid.Parse("F0C909C0-6914-4939-9449-7DA80C2481FA"), "Normal" });

            migrationBuilder.InsertData(
            table: "UserType",
            columns: new[] { "UserTypeId", "Name" },
            values: new object[] { Guid.Parse("1BC8FFC9-3208-4F87-8D24-17D3C0D477BE"), "SuperUser" });

            migrationBuilder.InsertData(
            table: "UserType",
            columns: new[] { "UserTypeId", "Name" },
            values: new object[] { Guid.Parse("B5189434-5E6C-4185-816E-E38DA2A8F8BE"), "Premium" });


            migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "UserId", "Name", "Email", "Address", "Phone", "UserTypeId", "Money" },
            values: new object[] { Guid.NewGuid(), "Juan", "juan@marmol.com", "Peru 2464", "+5491154762312", 
                Guid.Parse("F0C909C0-6914-4939-9449-7DA80C2481FA"), Convert.ToDecimal(12.34) });

            migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "UserId", "Name", "Email", "Address", "Phone", "UserTypeId", "Money" },
            values: new object[] { Guid.NewGuid(), "Franco", "franco.perez@gmail.com", "Alvear y Colombres", "+534645213542",
                Guid.Parse("B5189434-5E6C-4185-816E-E38DA2A8F8BE"), Convert.ToDecimal(1122.34) });

            migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "UserId", "Name", "Email", "Address", "Phone", "UserTypeId", "Money" },
            values: new object[] { Guid.NewGuid(), "Agustina", "agustina@gmail.com", "Garay y Otra Calle", "+534645213542",
                Guid.Parse("1BC8FFC9-3208-4F87-8D24-17D3C0D477BE"), Convert.ToDecimal(1122.34) });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
