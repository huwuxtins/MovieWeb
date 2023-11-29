using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWeb.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CastModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearRelease = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkDemo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmLength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CastModelFilmModel",
                columns: table => new
                {
                    CastsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastModelFilmModel", x => new { x.CastsId, x.FilmsId });
                    table.ForeignKey(
                        name: "FK_CastModelFilmModel_CastModels_CastsId",
                        column: x => x.CastsId,
                        principalTable: "CastModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastModelFilmModel_FilmModels_FilmsId",
                        column: x => x.FilmsId,
                        principalTable: "FilmModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentModels_FilmModels_FilmId",
                        column: x => x.FilmId,
                        principalTable: "FilmModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentModels_UserModels_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastModelFilmModel_FilmsId",
                table: "CastModelFilmModel",
                column: "FilmsId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentModels_FilmId",
                table: "CommentModels",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentModels_UserId",
                table: "CommentModels",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastModelFilmModel");

            migrationBuilder.DropTable(
                name: "CommentModels");

            migrationBuilder.DropTable(
                name: "CastModels");

            migrationBuilder.DropTable(
                name: "FilmModels");

            migrationBuilder.DropTable(
                name: "UserModels");
        }
    }
}
