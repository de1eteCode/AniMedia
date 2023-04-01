using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniMedia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addavatarfiletouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarLink",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "AvatarFileUID",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: new Guid("757cafc6-b801-490f-87f8-f07e75fdb834"),
                column: "AvatarFileUID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: new Guid("9fd09841-ef23-4b37-9237-8eed04ff8d8d"),
                column: "AvatarFileUID",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarFileUID",
                table: "Users",
                column: "AvatarFileUID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BinaryFiles_AvatarFileUID",
                table: "Users",
                column: "AvatarFileUID",
                principalTable: "BinaryFiles",
                principalColumn: "UID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BinaryFiles_AvatarFileUID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AvatarFileUID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarFileUID",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "AvatarLink",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: new Guid("757cafc6-b801-490f-87f8-f07e75fdb834"),
                column: "AvatarLink",
                value: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UID",
                keyValue: new Guid("9fd09841-ef23-4b37-9237-8eed04ff8d8d"),
                column: "AvatarLink",
                value: "");
        }
    }
}
