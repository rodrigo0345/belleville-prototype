using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class Outro3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Imovel",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Chave",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("30b3b9b0-60ca-45eb-a1ea-f0db8c4f116f"), null, "Admin", "ADMIN" },
                    { new Guid("996796f8-a842-473c-a365-cbded013a32f"), null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("045f5db4-aefb-4465-8ba4-8f8599c330d6"), 0, "c32d9fc6-c8ce-4a62-9ce1-42a80037c28f", "admin@gmail.com", true, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKMtl/INSm9F9jJeSa8W8F1VyDdsShFrC66To/nUYjAxD1ZfeHGtxwIx6kAVLMHd7w==", "", null, false, "5840e6f2-e1f2-4954-a059-3b8cbcc2e0df", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("30b3b9b0-60ca-45eb-a1ea-f0db8c4f116f"), new Guid("045f5db4-aefb-4465-8ba4-8f8599c330d6") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("996796f8-a842-473c-a365-cbded013a32f"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("30b3b9b0-60ca-45eb-a1ea-f0db8c4f116f"), new Guid("045f5db4-aefb-4465-8ba4-8f8599c330d6") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30b3b9b0-60ca-45eb-a1ea-f0db8c4f116f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("045f5db4-aefb-4465-8ba4-8f8599c330d6"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Imovel");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Chave");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

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
        }
    }
}
