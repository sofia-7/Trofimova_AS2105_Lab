using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class Postman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Bouqet_BouqetId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BouqetId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_BouqetId",
                table: "Orders",
                column: "BouqetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Bouqet_BouqetId",
                table: "Orders",
                column: "BouqetId",
                principalTable: "Bouqet",
                principalColumn: "BouqetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
