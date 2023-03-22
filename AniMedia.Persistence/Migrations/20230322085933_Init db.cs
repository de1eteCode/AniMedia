using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AniMedia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uuid", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    SecondName = table.Column<string>(type: "text", nullable: true),
                    AvatarLink = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUid = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<Guid>(type: "uuid", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    UserAgent = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.UID);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserUid",
                        column: x => x.UserUid,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UID", "AvatarLink", "FirstName", "Nickname", "PasswordHash", "PasswordSalt", "SecondName" },
                values: new object[,]
                {
                    { new Guid("757cafc6-b801-490f-87f8-f07e75fdb834"), "", "", "common", "iDR0tLFtovM0IcDvxocHaCRCZZ6RDO3HVuUx3pIO0vDw3WP5qlLBSjVI3PmaflP5G1dVZEfE3oS4KB8IaUVQwg==", "wwkDoDuq1buKb/Cca65BVfLEeNkp5axgOpXkd25kDs6uCEkhtpG16z9UXxtvNBC5UnbdfHPPyduPKHjdNNsbFvkBtVR176zu4YHJWqAl9nN9By1VsUZpf+jIR5/H40teb2y+oiATCbM+zhhaBbRK8N+JVf/KxWyfPtbpJCw84X0=", "" },
                    { new Guid("9fd09841-ef23-4b37-9237-8eed04ff8d8d"), "", "", "de1ete", "iDR0tLFtovM0IcDvxocHaCRCZZ6RDO3HVuUx3pIO0vDw3WP5qlLBSjVI3PmaflP5G1dVZEfE3oS4KB8IaUVQwg==", "wwkDoDuq1buKb/Cca65BVfLEeNkp5axgOpXkd25kDs6uCEkhtpG16z9UXxtvNBC5UnbdfHPPyduPKHjdNNsbFvkBtVR176zu4YHJWqAl9nN9By1VsUZpf+jIR5/H40teb2y+oiATCbM+zhhaBbRK8N+JVf/KxWyfPtbpJCw84X0=", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserUid",
                table: "Sessions",
                column: "UserUid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
