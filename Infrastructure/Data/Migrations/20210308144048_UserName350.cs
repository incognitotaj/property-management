using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class UserName350 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "Identity",
                table: "User",
                type: "VARCHAR(350)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(25)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "Identity",
                table: "User",
                type: "VARCHAR(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(350)");
        }
    }
}
