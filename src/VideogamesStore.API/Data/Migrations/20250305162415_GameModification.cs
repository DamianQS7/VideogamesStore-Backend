using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogamesStore.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class GameModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Games");
        }
    }
}
