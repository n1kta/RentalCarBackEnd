using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalCar.Migrations
{
    public partial class UpdateRentalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentDate",
                table: "Rentals");

            migrationBuilder.AddColumn<DateTime>(
                name: "DropOffDate",
                table: "Rentals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpDate",
                table: "Rentals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOffDate",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "PickUpDate",
                table: "Rentals");

            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentDate",
                table: "Rentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
