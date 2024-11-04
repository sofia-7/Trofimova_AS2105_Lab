using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MonoFlowers",
                newName: "MonoFlowerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bouqet",
                newName: "BouqetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonoFlowerId",
                table: "MonoFlowers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BouqetId",
                table: "Bouqet",
                newName: "Id");
        }
    }
}
