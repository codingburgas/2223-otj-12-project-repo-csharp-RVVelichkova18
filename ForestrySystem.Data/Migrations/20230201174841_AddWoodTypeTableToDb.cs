using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForestrySystem.Migrations
{
    /// <inheritdoc />
    public partial class AddWoodTypeTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WoodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SpeciesName = table.Column<string>(type: "TEXT", nullable: false),
                    Origin = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountForLogging = table.Column<float>(type: "REAL", nullable: false),
                    YearOfLogging = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoodTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WoodTypes");
        }
    }
}
