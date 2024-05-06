using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AdminAccount02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("2afeffaf-d66f-4931-a81d-121ae70bbe3b"), null, "Admin", "ADMIN" },
                    { new Guid("63ada8a2-4e02-463a-a7a2-6e634dfab4e9"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("49dbf1f6-e249-4a95-be90-5fefb9ccee5c"), 0, "43d53154-280e-4230-83f4-d571b2491272", "admin@gmail.com", true, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEGRkr9N+d6/MFsGOC5YKFWndOdrlDEdl0ota3hvz2SATdy3ZbTs5Oysl9kahU2rvZQ==", "", null, false, "fab49051-436e-4406-878d-bf4fd3ec214a", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("2afeffaf-d66f-4931-a81d-121ae70bbe3b"), new Guid("49dbf1f6-e249-4a95-be90-5fefb9ccee5c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("63ada8a2-4e02-463a-a7a2-6e634dfab4e9"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2afeffaf-d66f-4931-a81d-121ae70bbe3b"), new Guid("49dbf1f6-e249-4a95-be90-5fefb9ccee5c") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2afeffaf-d66f-4931-a81d-121ae70bbe3b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("49dbf1f6-e249-4a95-be90-5fefb9ccee5c"));

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
    }
}
