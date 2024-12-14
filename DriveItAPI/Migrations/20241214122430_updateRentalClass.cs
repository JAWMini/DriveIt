using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveItAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateRentalClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedDate",
                table: "Rents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Rents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedDate",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rents");
        }
    }
}
