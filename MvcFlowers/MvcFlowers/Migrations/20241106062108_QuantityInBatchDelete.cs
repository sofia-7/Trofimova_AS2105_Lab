using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFlowers.Migrations
{
    /// <inheritdoc />
    public partial class QuantityInBatchDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityInBatch",
                table: "MonoFlowers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityInBatch",
                table: "MonoFlowers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
