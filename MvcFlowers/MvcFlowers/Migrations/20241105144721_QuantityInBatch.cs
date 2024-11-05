using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class QuantityInBatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bouqets");

            migrationBuilder.DropTable(
                name: "PottedFlowers");

            migrationBuilder.AddColumn<int>(
                name: "QuantityInBatch",
                table: "MonoFlowers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityInBatch",
                table: "MonoFlowers");

            migrationBuilder.CreateTable(
                name: "Bouqets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Flower_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bouqets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PottedFlowers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lightning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RecievementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PottedFlowers", x => x.Id);
                });
        }
    }
}
