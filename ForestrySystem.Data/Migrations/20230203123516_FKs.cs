using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForestrySystem.Migrations
{
    /// <inheritdoc />
    public partial class FKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "SiteViewers",
                newName: "InstitutionId");

            migrationBuilder.AddColumn<int>(
                name: "FIRefID",
                table: "SiteViewers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "SiteViewers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FIEventRefID",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstitutionsId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_InstitutionId",
                table: "SiteViewers",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_InstitutionsId",
                table: "Events",
                column: "InstitutionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Institutions_InstitutionsId",
                table: "Events",
                column: "InstitutionsId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Institutions_InstitutionId",
                table: "SiteViewers",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Institutions_InstitutionsId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Institutions_InstitutionId",
                table: "SiteViewers");

            migrationBuilder.DropIndex(
                name: "IX_Users_InstitutionId",
                table: "SiteViewers");

            migrationBuilder.DropIndex(
                name: "IX_Events_InstitutionsId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FIRefID",
                table: "SiteViewers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "SiteViewers");

            migrationBuilder.DropColumn(
                name: "FIEventRefID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "InstitutionsId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "InstitutionId",
                table: "SiteViewers",
                newName: "isDeleted");
        }
    }
}
