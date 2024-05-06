using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AdminAccount021 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
