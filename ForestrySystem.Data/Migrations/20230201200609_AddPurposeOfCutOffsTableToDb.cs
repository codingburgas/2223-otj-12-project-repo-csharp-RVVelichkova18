using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForestrySystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPurposeOfCutOffsTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurposeOfCutOff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Purpose = table.Column<string>(type: "TEXT", nullable: false),
                    PercentagePerYear = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurposeOfCutOff", x => x.Id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurposeOfCutOff");

           
        }
    }
}
