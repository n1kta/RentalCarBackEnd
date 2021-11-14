using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalCar.Migrations
{
    public partial class AddRentalLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DropOffLoc",
                table: "Rentals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpLoc",
                table: "Rentals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOffLoc",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "PickUpLoc",
                table: "Rentals");
        }
    }
}
