using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveIt.Migrations.CarRental
{
    /// <inheritdoc />
    public partial class UpdateRental1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Car_Brand",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Car_City",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Car_Id",
                table: "Rentals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Car_Model",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Car_Year",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Car_Brand",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Car_City",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Car_Id",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Car_Model",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Car_Year",
                table: "Rentals");
        }
    }
}
