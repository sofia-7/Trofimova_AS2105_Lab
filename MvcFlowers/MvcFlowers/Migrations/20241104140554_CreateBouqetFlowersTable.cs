using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class CreateBouqetFlowersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonoFlowers_Bouqet_BouqetId",
                table: "MonoFlowers");

            migrationBuilder.DropIndex(
                name: "IX_MonoFlowers_BouqetId",
                table: "MonoFlowers");

            migrationBuilder.DropColumn(
                name: "BouqetId",
                table: "MonoFlowers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MonoFlowers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "BouqetFlowers",
                columns: table => new
                {
                    BouqetId = table.Column<int>(type: "int", nullable: false),
                    FlowersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BouqetFlowers", x => new { x.BouqetId, x.FlowersId });
                    table.ForeignKey(
                        name: "FK_BouqetFlowers_Bouqet_BouqetId",
                        column: x => x.BouqetId,
                        principalTable: "Bouqet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BouqetFlowers_MonoFlowers_FlowersId",
                        column: x => x.FlowersId,
                        principalTable: "MonoFlowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BouqetFlowers_FlowersId",
                table: "BouqetFlowers",
                column: "FlowersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BouqetFlowers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MonoFlowers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "BouqetId",
                table: "MonoFlowers",
                type: "int",
                nullable: true);

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
    }
}
