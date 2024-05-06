using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("1170c28f-7a41-4012-b369-bff54897a5cc"), null, "User", "USER" },
                    { new Guid("9b4a1467-815e-4a6b-834e-fe5db6e8d7f4"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("4682edb1-42ba-490e-b718-7b506a1cc514"), 0, "96e54658-526c-476f-9f06-5987713e3c31", "admin@gmail.com", true, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKkQTz+gOl/FHX26QjYaytNS9FVzSTIYKUauHxqeKFWAPW2cltMFM8Uj2oUQcqdKTg==", "", null, false, "9a104b85-403f-4c40-bbec-1c567982b456", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1170c28f-7a41-4012-b369-bff54897a5cc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9b4a1467-815e-4a6b-834e-fe5db6e8d7f4"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4682edb1-42ba-490e-b718-7b506a1cc514"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("62216533-7b86-42dc-b2de-569f8555197d"), null, "User", "USER" },
                    { new Guid("db9a7d88-7f6d-409d-b68d-a0676bc8935a"), null, "Admin", "ADMIN" }
                });
        }
    }
}
