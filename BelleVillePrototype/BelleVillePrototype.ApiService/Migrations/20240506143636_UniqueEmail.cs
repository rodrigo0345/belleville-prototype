using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class UniqueEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b94a74e3-1545-4470-bef8-9179c84c277a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb722e27-8098-43d9-ab6c-2a2671ba1823"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("913bdc6f-ba36-4430-a7e6-8cef3dff8f04"), null, "Admin", "ADMIN" },
                    { new Guid("dffc8e96-755f-4d39-8e22-61cbc4cad2cc"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("913bdc6f-ba36-4430-a7e6-8cef3dff8f04"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dffc8e96-755f-4d39-8e22-61cbc4cad2cc"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b94a74e3-1545-4470-bef8-9179c84c277a"), null, "Admin", "ADMIN" },
                    { new Guid("fb722e27-8098-43d9-ab6c-2a2671ba1823"), null, "User", "USER" }
                });
        }
    }
}
