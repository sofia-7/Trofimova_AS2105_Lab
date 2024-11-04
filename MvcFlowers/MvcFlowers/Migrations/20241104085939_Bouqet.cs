using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class Bouqet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BouqetId",
                table: "MonoFlowers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bouqet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bouqet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonoFlowers_BouqetId",
                table: "MonoFlowers",
                column: "BouqetId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonoFlowers_Bouqet_BouqetId",
                table: "MonoFlowers",
                column: "BouqetId",
                principalTable: "Bouqet",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonoFlowers_Bouqet_BouqetId",
                table: "MonoFlowers");

            migrationBuilder.DropTable(
                name: "Bouqet");

            migrationBuilder.DropIndex(
                name: "IX_MonoFlowers_BouqetId",
                table: "MonoFlowers");

            migrationBuilder.DropColumn(
                name: "BouqetId",
                table: "MonoFlowers");
        }
    }
}
