using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalCar.Migrations
{
    public partial class AddYearAndFuelToCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Car_CarId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Customer_CustomerId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Return_Rental_RentalId",
                table: "Return");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Return",
                table: "Return");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rental",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Return",
                newName: "Returns");

            migrationBuilder.RenameTable(
                name: "Rental",
                newName: "Rentals");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_Return_RentalId",
                table: "Returns",
                newName: "IX_Returns_RentalId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_CustomerId",
                table: "Rentals",
                newName: "IX_Rentals_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_CarId",
                table: "Rentals",
                newName: "IX_Rentals_CarId");

            migrationBuilder.AddColumn<int>(
                name: "Fuel",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Year",
                table: "Cars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Returns",
                table: "Returns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Cars_CarId",
                table: "Rentals",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Customer_CustomerId",
                table: "Rentals",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Returns_Rentals_RentalId",
                table: "Returns",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Cars_CarId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Customer_CustomerId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Returns_Rentals_RentalId",
                table: "Returns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Returns",
                table: "Returns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Fuel",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Returns",
                newName: "Return");

            migrationBuilder.RenameTable(
                name: "Rentals",
                newName: "Rental");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Returns_RentalId",
                table: "Return",
                newName: "IX_Return_RentalId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_CustomerId",
                table: "Rental",
                newName: "IX_Rental_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_CarId",
                table: "Rental",
                newName: "IX_Rental_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Return",
                table: "Return",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rental",
                table: "Rental",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Car_CarId",
                table: "Rental",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Customer_CustomerId",
                table: "Rental",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Return_Rental_RentalId",
                table: "Return",
                column: "RentalId",
                principalTable: "Rental",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
