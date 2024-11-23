using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class Manager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowerInBouqet_Bouqet_BouqetId",
                table: "FlowerInBouqet");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowerInBouqet_Flowers_FlowerId",
                table: "FlowerInBouqet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlowerInBouqet",
                table: "FlowerInBouqet");

            migrationBuilder.DropIndex(
                name: "IX_FlowerInBouqet_FlowerId",
                table: "FlowerInBouqet");

            migrationBuilder.RenameTable(
                name: "FlowerInBouqet",
                newName: "FlowerInBouqets");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FlowerInBouqets",
                newName: "FlowerId1");

            migrationBuilder.RenameIndex(
                name: "IX_FlowerInBouqet_BouqetId",
                table: "FlowerInBouqets",
                newName: "IX_FlowerInBouqets_BouqetId");

            migrationBuilder.AlterColumn<int>(
                name: "FlowerId",
                table: "FlowerInBouqets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "FlowerId1",
                table: "FlowerInBouqets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlowerInBouqets",
                table: "FlowerInBouqets",
                column: "FlowerId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerInBouqets_FlowerId1",
                table: "FlowerInBouqets",
                column: "FlowerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowerInBouqets_Bouqet_BouqetId",
                table: "FlowerInBouqets",
                column: "BouqetId",
                principalTable: "Bouqet",
                principalColumn: "BouqetId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowerInBouqets_Flowers_FlowerId1",
                table: "FlowerInBouqets",
                column: "FlowerId1",
                principalTable: "Flowers",
                principalColumn: "FlowerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlowerInBouqets_Bouqet_BouqetId",
                table: "FlowerInBouqets");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowerInBouqets_Flowers_FlowerId1",
                table: "FlowerInBouqets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlowerInBouqets",
                table: "FlowerInBouqets");

            migrationBuilder.DropIndex(
                name: "IX_FlowerInBouqets_FlowerId1",
                table: "FlowerInBouqets");

            migrationBuilder.RenameTable(
                name: "FlowerInBouqets",
                newName: "FlowerInBouqet");

            migrationBuilder.RenameColumn(
                name: "FlowerId1",
                table: "FlowerInBouqet",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_FlowerInBouqets_BouqetId",
                table: "FlowerInBouqet",
                newName: "IX_FlowerInBouqet_BouqetId");

            migrationBuilder.AlterColumn<int>(
                name: "FlowerId",
                table: "FlowerInBouqet",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FlowerInBouqet",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlowerInBouqet",
                table: "FlowerInBouqet",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerInBouqet_FlowerId",
                table: "FlowerInBouqet",
                column: "FlowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowerInBouqet_Bouqet_BouqetId",
                table: "FlowerInBouqet",
                column: "BouqetId",
                principalTable: "Bouqet",
                principalColumn: "BouqetId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowerInBouqet_Flowers_FlowerId",
                table: "FlowerInBouqet",
                column: "FlowerId",
                principalTable: "Flowers",
                principalColumn: "FlowerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
