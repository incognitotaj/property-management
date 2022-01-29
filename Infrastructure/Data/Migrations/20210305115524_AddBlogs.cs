using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddBlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Lookup");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Lookup",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                schema: "Property",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(2000)", nullable: false),
                    Created = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    Photo = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false),
                    CategoryId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blog_Category",
                        column: x => x.CategoryId,
                        principalSchema: "Lookup",
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_CategoryId",
                schema: "Property",
                table: "Blog",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog",
                schema: "Property");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Lookup");
        }
    }
}
