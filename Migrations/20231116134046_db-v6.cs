using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWeb.Migrations
{
    /// <inheritdoc />
    public partial class dbv6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "FilmModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<int>(
                name: "Reviews",
                table: "FilmModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reviews",
                table: "FilmModels");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "FilmModels",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
