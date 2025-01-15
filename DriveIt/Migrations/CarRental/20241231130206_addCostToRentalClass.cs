using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveIt.Migrations.CarRental
{
    /// <inheritdoc />
    public partial class addCostToRentalClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Rentals",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Rentals");
        }
    }
}
