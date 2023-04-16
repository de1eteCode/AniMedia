using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniMedia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addanimeseriesmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeSeries",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    EnglishName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    JapaneseName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateOfRelease = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateOfAnnouncement = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExistTotalEpisodes = table.Column<int>(type: "integer", nullable: true),
                    PlanedTotalEpisodes = table.Column<int>(type: "integer", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateByUid = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedByUid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSeries", x => x.UID);
                    table.ForeignKey(
                        name: "FK_AnimeSeries_Users_CreateByUid",
                        column: x => x.CreateByUid,
                        principalTable: "Users",
                        principalColumn: "UID");
                    table.ForeignKey(
                        name: "FK_AnimeSeries_Users_LastModifiedByUid",
                        column: x => x.LastModifiedByUid,
                        principalTable: "Users",
                        principalColumn: "UID");
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateByUid = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedByUid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.UID);
                    table.ForeignKey(
                        name: "FK_Genres_Users_CreateByUid",
                        column: x => x.CreateByUid,
                        principalTable: "Users",
                        principalColumn: "UID");
                    table.ForeignKey(
                        name: "FK_Genres_Users_LastModifiedByUid",
                        column: x => x.LastModifiedByUid,
                        principalTable: "Users",
                        principalColumn: "UID");
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimeSeriesUid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUid = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<byte>(type: "smallint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateByUid = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedByUid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => new { x.UID, x.AnimeSeriesUid, x.UserUid });
                    table.ForeignKey(
                        name: "FK_Rates_AnimeSeries_AnimeSeriesUid",
                        column: x => x.AnimeSeriesUid,
                        principalTable: "AnimeSeries",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rates_AnimeSeries_UID",
                        column: x => x.UID,
                        principalTable: "AnimeSeries",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rates_Users_CreateByUid",
                        column: x => x.CreateByUid,
                        principalTable: "Users",
                        principalColumn: "UID");
                    table.ForeignKey(
                        name: "FK_Rates_Users_LastModifiedByUid",
                        column: x => x.LastModifiedByUid,
                        principalTable: "Users",
                        principalColumn: "UID");
                    table.ForeignKey(
                        name: "FK_Rates_Users_UserUid",
                        column: x => x.UserUid,
                        principalTable: "Users",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeSeriesGenres",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreUid = table.Column<Guid>(type: "uuid", nullable: false),
                    AnimeSeriesUid = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreEntityUID = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateByUid = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedByUid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSeriesGenres", x => new { x.UID, x.GenreUid, x.AnimeSeriesUid });
                    table.ForeignKey(
                        name: "FK_AnimeSeriesGenres_AnimeSeries_AnimeSeriesUid",
                        column: x => x.AnimeSeriesUid,
                        principalTable: "AnimeSeries",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeSeriesGenres_AnimeSeries_UID",
                        column: x => x.UID,
                        principalTable: "AnimeSeries",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeSeriesGenres_Genres_GenreEntityUID",
                        column: x => x.GenreEntityUID,
                        principalTable: "Genres",
                        principalColumn: "UID");
                    table.ForeignKey(
                        name: "FK_AnimeSeriesGenres_Genres_GenreUid",
                        column: x => x.GenreUid,
                        principalTable: "Genres",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeSeriesGenres_Users_CreateByUid",
                        column: x => x.CreateByUid,
                        principalTable: "Users",
                        principalColumn: "UID");
                    table.ForeignKey(
                        name: "FK_AnimeSeriesGenres_Users_LastModifiedByUid",
                        column: x => x.LastModifiedByUid,
                        principalTable: "Users",
                        principalColumn: "UID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSeries_CreateByUid",
                table: "AnimeSeries",
                column: "CreateByUid");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSeries_LastModifiedByUid",
                table: "AnimeSeries",
                column: "LastModifiedByUid");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSeriesGenres_AnimeSeriesUid",
                table: "AnimeSeriesGenres",
                column: "AnimeSeriesUid");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSeriesGenres_CreateByUid",
                table: "AnimeSeriesGenres",
                column: "CreateByUid");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSeriesGenres_GenreEntityUID",
                table: "AnimeSeriesGenres",
                column: "GenreEntityUID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSeriesGenres_GenreUid",
                table: "AnimeSeriesGenres",
                column: "GenreUid");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSeriesGenres_LastModifiedByUid",
                table: "AnimeSeriesGenres",
                column: "LastModifiedByUid");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_CreateByUid",
                table: "Genres",
                column: "CreateByUid");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_LastModifiedByUid",
                table: "Genres",
                column: "LastModifiedByUid");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_AnimeSeriesUid",
                table: "Rates",
                column: "AnimeSeriesUid");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_CreateByUid",
                table: "Rates",
                column: "CreateByUid");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_LastModifiedByUid",
                table: "Rates",
                column: "LastModifiedByUid");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserUid",
                table: "Rates",
                column: "UserUid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeSeriesGenres");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "AnimeSeries");
        }
    }
}
