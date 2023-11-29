using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWeb.Migrations
{
    /// <inheritdoc />
    public partial class dbv10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastModelFilmModel");

            migrationBuilder.DropTable(
                name: "CastModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CastModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastModels", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_CastModelFilmModel_FilmsId",
                table: "CastModelFilmModel",
                column: "FilmsId");
        }
    }
}
