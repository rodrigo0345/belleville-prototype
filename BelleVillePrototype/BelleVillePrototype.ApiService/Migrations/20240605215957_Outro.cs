using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class Outro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b3292c9a-e632-4016-b0e9-67e37a75cec4"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6f0469f5-fd60-4a2a-8a06-1f3e7f8b0aba"), new Guid("b6f4dad2-65a3-4ed1-a325-0a8c962b09b1") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6f0469f5-fd60-4a2a-8a06-1f3e7f8b0aba"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b6f4dad2-65a3-4ed1-a325-0a8c962b09b1"));

            migrationBuilder.CreateTable(
                name: "Imovel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Morada = table.Column<string>(type: "text", nullable: false),
                    Localidade = table.Column<string>(type: "text", nullable: true),
                    CodigoPostal = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Classificacao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imovel", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("781ba9bf-b61c-4c8d-8f32-5b2f3dc85d8f"), null, "User", "USER" },
                    { new Guid("8920d2e3-fd40-4a33-8405-64a1300a1ccb"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("80677d16-88b1-4cea-8b2a-253d655c5e72"), 0, "25deb18b-c1cb-4ceb-bc25-dfa0314e55b9", "admin@gmail.com", true, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAELgUoNeqpkB3M1LkZZwUsxHbhnGRF6x8JAjxfHhsDXFedsDxn0AnGiEjPsl0/E+YjA==", "", null, false, "aa62880a-643d-45b1-af5d-57afc0e95763", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8920d2e3-fd40-4a33-8405-64a1300a1ccb"), new Guid("80677d16-88b1-4cea-8b2a-253d655c5e72") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imovel");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("781ba9bf-b61c-4c8d-8f32-5b2f3dc85d8f"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8920d2e3-fd40-4a33-8405-64a1300a1ccb"), new Guid("80677d16-88b1-4cea-8b2a-253d655c5e72") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8920d2e3-fd40-4a33-8405-64a1300a1ccb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("80677d16-88b1-4cea-8b2a-253d655c5e72"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6f0469f5-fd60-4a2a-8a06-1f3e7f8b0aba"), null, "Admin", "ADMIN" },
                    { new Guid("b3292c9a-e632-4016-b0e9-67e37a75cec4"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b6f4dad2-65a3-4ed1-a325-0a8c962b09b1"), 0, "15bc1e97-fb8d-437f-9275-a329a643f0ea", "admin@gmail.com", true, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEHgfGIHOnTGI70hhas2FcFkOIzDyRuxOeiTxx9d5BVuKRmiCVL/Z4gSoGCv/VbBYIQ==", "", null, false, "9d73e154-2869-41f3-a8c6-d7cb7eb7fefb", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("6f0469f5-fd60-4a2a-8a06-1f3e7f8b0aba"), new Guid("b6f4dad2-65a3-4ed1-a325-0a8c962b09b1") });
        }
    }
}
