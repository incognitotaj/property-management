using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class ServiceRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                schema: "Property",
                columns: table => new
                {
                    ServiceRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(350)", nullable: false),
                    Mobile = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    Message = table.Column<string>(type: "VARCHAR(2000)", nullable: false),
                    ServiceId = table.Column<int>(type: "INT", nullable: false),
                    IsRead = table.Column<bool>(type: "BIT", nullable: false),
                    Created = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequest", x => x.ServiceRequestId);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_Service",
                        column: x => x.ServiceId,
                        principalSchema: "Property",
                        principalTable: "Service",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_ServiceId",
                schema: "Property",
                table: "ServiceRequest",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequest",
                schema: "Property");
        }
    }
}
