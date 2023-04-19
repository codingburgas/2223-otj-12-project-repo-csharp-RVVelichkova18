using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForestrySystem.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeOfTimberTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeOfTimber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimberName = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountForLogging = table.Column<float>(type: "REAL", nullable: false),
                    YearOfLogging = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfTimber", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypeOfTimber");
        }
    }
}
