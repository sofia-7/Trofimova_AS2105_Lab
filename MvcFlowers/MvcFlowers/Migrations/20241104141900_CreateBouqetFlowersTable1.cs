using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class CreateBouqetFlowersTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BouqetFlowers_MonoFlowers_FlowersId",
                table: "BouqetFlowers");

            migrationBuilder.RenameColumn(
                name: "FlowersId",
                table: "BouqetFlowers",
                newName: "MonoFlowerId");

            migrationBuilder.RenameIndex(
                name: "IX_BouqetFlowers_FlowersId",
                table: "BouqetFlowers",
                newName: "IX_BouqetFlowers_MonoFlowerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MonoFlowers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_BouqetFlowers_MonoFlowers_MonoFlowerId",
                table: "BouqetFlowers",
                column: "MonoFlowerId",
                principalTable: "MonoFlowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BouqetFlowers_MonoFlowers_MonoFlowerId",
                table: "BouqetFlowers");

            migrationBuilder.RenameColumn(
                name: "MonoFlowerId",
                table: "BouqetFlowers",
                newName: "FlowersId");

            migrationBuilder.RenameIndex(
                name: "IX_BouqetFlowers_MonoFlowerId",
                table: "BouqetFlowers",
                newName: "IX_BouqetFlowers_FlowersId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MonoFlowers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_BouqetFlowers_MonoFlowers_FlowersId",
                table: "BouqetFlowers",
                column: "FlowersId",
                principalTable: "MonoFlowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
