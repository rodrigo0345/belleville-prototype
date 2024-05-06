using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class UniqueEmail02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("62216533-7b86-42dc-b2de-569f8555197d"), null, "User", "USER" },
                    { new Guid("db9a7d88-7f6d-409d-b68d-a0676bc8935a"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("62216533-7b86-42dc-b2de-569f8555197d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("db9a7d88-7f6d-409d-b68d-a0676bc8935a"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("913bdc6f-ba36-4430-a7e6-8cef3dff8f04"), null, "Admin", "ADMIN" },
                    { new Guid("dffc8e96-755f-4d39-8e22-61cbc4cad2cc"), null, "User", "USER" }
                });
        }
    }
}
