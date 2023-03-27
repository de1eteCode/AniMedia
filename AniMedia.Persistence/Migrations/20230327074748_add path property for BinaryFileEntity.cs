using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniMedia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addpathpropertyforBinaryFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathFile",
                table: "BinaryFiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathFile",
                table: "BinaryFiles");
        }
    }
}
