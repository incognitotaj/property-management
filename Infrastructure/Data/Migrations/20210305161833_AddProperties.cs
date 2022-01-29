using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaUnit",
                schema: "Lookup",
                columns: table => new
                {
                    AreaUnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaUnit", x => x.AreaUnitId);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "Lookup",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "PropertyType",
                schema: "Lookup",
                columns: table => new
                {
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyType", x => x.PropertyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Purpose",
                schema: "Lookup",
                columns: table => new
                {
                    PurposeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purpose", x => x.PurposeId);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                schema: "Property",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(2000)", nullable: false),
                    PropertyTypeId = table.Column<int>(type: "INT", nullable: false),
                    PurposeId = table.Column<int>(type: "INT", nullable: false),
                    CityId = table.Column<int>(type: "INT", nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(2000)", nullable: false),
                    Price = table.Column<decimal>(type: "NUMERIC(18,2)", nullable: false),
                    LandArea = table.Column<decimal>(type: "NUMERIC(18,2)", nullable: false),
                    AreaUnitId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(350)", nullable: false),
                    Mobile = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    Created = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Property_AreaUnit",
                        column: x => x.AreaUnitId,
                        principalSchema: "Lookup",
                        principalTable: "AreaUnit",
                        principalColumn: "AreaUnitId");
                    table.ForeignKey(
                        name: "FK_Property_City",
                        column: x => x.CityId,
                        principalSchema: "Lookup",
                        principalTable: "City",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Property_PropertyType",
                        column: x => x.PropertyTypeId,
                        principalSchema: "Lookup",
                        principalTable: "PropertyType",
                        principalColumn: "PropertyTypeId");
                    table.ForeignKey(
                        name: "FK_Property_Purpose",
                        column: x => x.PurposeId,
                        principalSchema: "Lookup",
                        principalTable: "Purpose",
                        principalColumn: "PurposeId");
                });

            migrationBuilder.CreateTable(
                name: "PropertyPhoto",
                schema: "Property",
                columns: table => new
                {
                    PropertyPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "INT", nullable: false),
                    Photo = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    Created = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPhoto", x => x.PropertyPhotoId);
                    table.ForeignKey(
                        name: "FK_PropertyPhoto_Property",
                        column: x => x.PropertyId,
                        principalSchema: "Property",
                        principalTable: "Property",
                        principalColumn: "PropertyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_AreaUnitId",
                schema: "Property",
                table: "Property",
                column: "AreaUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_CityId",
                schema: "Property",
                table: "Property",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_PropertyTypeId",
                schema: "Property",
                table: "Property",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_PurposeId",
                schema: "Property",
                table: "Property",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyPhoto_PropertyId",
                schema: "Property",
                table: "PropertyPhoto",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyPhoto",
                schema: "Property");

            migrationBuilder.DropTable(
                name: "Property",
                schema: "Property");

            migrationBuilder.DropTable(
                name: "AreaUnit",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "City",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "PropertyType",
                schema: "Lookup");

            migrationBuilder.DropTable(
                name: "Purpose",
                schema: "Lookup");
        }
    }
}
