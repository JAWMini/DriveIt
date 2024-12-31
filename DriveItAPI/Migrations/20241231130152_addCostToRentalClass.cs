using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveItAPI.Migrations
{
    /// <inheritdoc />
    public partial class addCostToRentalClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Rents",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Rents");
        }
    }
}
