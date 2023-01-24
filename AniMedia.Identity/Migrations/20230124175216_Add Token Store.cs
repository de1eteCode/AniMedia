using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniMedia.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokenUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    DateOfExpired = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokenUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokenUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a6330a68-a4e3-466c-9a03-6b45c093f90f"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "03525d6d-9df7-4e9e-af93-6e25ee8a7bc3", "ADMIN@LOCALHOST.COM", "ADMIN", "AQAAAAIAAYagAAAAEPZwaiurByGbEveOMOxlAVRXM77V/BpEhdyAFiIyr8utis3yypR0y2wIHrjE3WVndA==" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokenUsers_UserId",
                table: "RefreshTokenUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokenUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a6330a68-a4e3-466c-9a03-6b45c093f90f"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "434a6d69-bdfa-41b3-96d9-9ddf9c71d1ca", "admin@localhost.com", "admin", "AQAAAAIAAYagAAAAENSkDGjP57BdimWIStx7RAz9xUuqbejE+m5ZFwRP6OxICUQWLPVGgvHmoDQrU0HHSw==" });
        }
    }
}
