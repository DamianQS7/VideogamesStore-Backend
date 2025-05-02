using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideogamesStore.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDetailsImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetailsImageUrl",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailsImageUrl",
                table: "Games");
        }
    }
}
