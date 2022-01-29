using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class PropertyRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyRequest",
                schema: "Property",
                columns: table => new
                {
                    PropertyRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(350)", nullable: false),
                    Mobile = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    Message = table.Column<string>(type: "VARCHAR(2000)", nullable: false),
                    PropertyId = table.Column<int>(type: "INT", nullable: false),
                    IsRead = table.Column<bool>(type: "BIT", nullable: false),
                    Created = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyRequest", x => x.PropertyRequestId);
                    table.ForeignKey(
                        name: "FK_PropertyRequest_Property",
                        column: x => x.PropertyId,
                        principalSchema: "Property",
                        principalTable: "Property",
                        principalColumn: "PropertyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRequest_PropertyId",
                schema: "Property",
                table: "PropertyRequest",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyRequest",
                schema: "Property");
        }
    }
}
