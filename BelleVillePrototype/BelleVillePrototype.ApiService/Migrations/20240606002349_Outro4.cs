using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BelleVillePrototype.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class Outro4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    ChaveId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Chave_ChaveId",
                        column: x => x.ChaveId,
                        principalTable: "Chave",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0070b43f-9464-4504-abfa-541334150bd5"), null, "User", "USER" },
                    { new Guid("a70ba982-bcc5-4814-90bc-dd3e47c2cad6"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("19964e71-211f-4256-ac63-5fd542c14daf"), 0, "860a662d-5832-4d84-b4c5-cdd0f3d0baae", "admin@gmail.com", true, "", "", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEEzIS7NKNL1Rlk/qIQQO3j3X+HPrr/losJWRp3DmT0Z5hNF7wfhIla/eduIU2rum0g==", "", null, false, "67961adc-3621-417e-a56a-363c66e80948", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a70ba982-bcc5-4814-90bc-dd3e47c2cad6"), new Guid("19964e71-211f-4256-ac63-5fd542c14daf") });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ChaveId",
                table: "Transaction",
                column: "ChaveId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0070b43f-9464-4504-abfa-541334150bd5"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a70ba982-bcc5-4814-90bc-dd3e47c2cad6"), new Guid("19964e71-211f-4256-ac63-5fd542c14daf") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a70ba982-bcc5-4814-90bc-dd3e47c2cad6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("19964e71-211f-4256-ac63-5fd542c14daf"));

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
    }
}
