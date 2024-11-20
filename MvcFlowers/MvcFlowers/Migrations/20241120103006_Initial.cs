using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BouqetFlowers");

            migrationBuilder.DropTable(
                name: "MonoFlowers");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Bouqet");

            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    FlowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.FlowerId);
                });

            migrationBuilder.CreateTable(
                name: "FlowerInBouqet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    BouqetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerInBouqet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowerInBouqet_Bouqet_BouqetId",
                        column: x => x.BouqetId,
                        principalTable: "Bouqet",
                        principalColumn: "BouqetId");
                    table.ForeignKey(
                        name: "FK_FlowerInBouqet_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "FlowerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerId = table.Column<int>(type: "int", nullable: false),
                    RecievementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packs_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "FlowerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerInBouqet_BouqetId",
                table: "FlowerInBouqet",
                column: "BouqetId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerInBouqet_FlowerId",
                table: "FlowerInBouqet",
                column: "FlowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_FlowerId",
                table: "Packs",
                column: "FlowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowerInBouqet");

            migrationBuilder.DropTable(
                name: "Packs");

            migrationBuilder.DropTable(
                name: "Flowers");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Bouqet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "MonoFlowers",
                columns: table => new
                {
                    MonoFlowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecievementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonoFlowers", x => x.MonoFlowerId);
                });

            migrationBuilder.CreateTable(
                name: "BouqetFlowers",
                columns: table => new
                {
                    BouqetId = table.Column<int>(type: "int", nullable: false),
                    MonoFlowerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BouqetFlowers", x => new { x.BouqetId, x.MonoFlowerId });
                    table.ForeignKey(
                        name: "FK_BouqetFlowers_Bouqet_BouqetId",
                        column: x => x.BouqetId,
                        principalTable: "Bouqet",
                        principalColumn: "BouqetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BouqetFlowers_MonoFlowers_MonoFlowerId",
                        column: x => x.MonoFlowerId,
                        principalTable: "MonoFlowers",
                        principalColumn: "MonoFlowerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BouqetFlowers_MonoFlowerId",
                table: "BouqetFlowers",
                column: "MonoFlowerId");
        }
    }
}
