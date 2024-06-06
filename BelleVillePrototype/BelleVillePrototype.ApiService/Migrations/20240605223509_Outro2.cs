using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class Outro2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Chave",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    ImovelId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chave", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chave_Imovel_ImovelId",
                        column: x => x.ImovelId,
                        principalTable: "Imovel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("696ae2a9-1429-42c8-89ed-1a85858f48fe"), null, "User", "USER" },
                    { new Guid("ee996c91-bd44-4d42-a108-4002c421b149"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("887fd27a-4474-4ef2-a463-d685d32eab39"), 0, "269715ef-c924-4d3d-b998-249787845a74", "admin@gmail.com", true, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEJhDnuvSLi9+4avAo8fuIka7oQMu4aSQ46B45cOV24xYd3c7+ubXMsePAmYUBm4/8g==", "", null, false, "35c4e152-b378-4ea2-a85e-b8b3760f328e", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ee996c91-bd44-4d42-a108-4002c421b149"), new Guid("887fd27a-4474-4ef2-a463-d685d32eab39") });

            migrationBuilder.CreateIndex(
                name: "IX_Chave_ImovelId",
                table: "Chave",
                column: "ImovelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chave");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("696ae2a9-1429-42c8-89ed-1a85858f48fe"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ee996c91-bd44-4d42-a108-4002c421b149"), new Guid("887fd27a-4474-4ef2-a463-d685d32eab39") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ee996c91-bd44-4d42-a108-4002c421b149"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("887fd27a-4474-4ef2-a463-d685d32eab39"));

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
    }
}
