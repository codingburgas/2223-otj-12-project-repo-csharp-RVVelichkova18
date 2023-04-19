using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForestrySystem.Migrations
{
    /// <inheritdoc />
    public partial class U2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "SiteViewers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "SiteViewers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
