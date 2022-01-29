using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class BedsBathKitchen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BathRooms",
                schema: "Property",
                table: "Property",
                type: "INT",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<int>(
                name: "BedRooms",
                schema: "Property",
                table: "Property",
                type: "INT",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<int>(
                name: "Floors",
                schema: "Property",
                table: "Property",
                type: "INT",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<int>(
                name: "Kitchens",
                schema: "Property",
                table: "Property",
                type: "INT",
                nullable: false,
                defaultValueSql: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BathRooms",
                schema: "Property",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "BedRooms",
                schema: "Property",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Floors",
                schema: "Property",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Kitchens",
                schema: "Property",
                table: "Property");
        }
    }
}
