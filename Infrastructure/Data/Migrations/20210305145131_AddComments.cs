using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "Property",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(350)", nullable: false),
                    Message = table.Column<string>(type: "VARCHAR(2000)", nullable: false),
                    Created = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Blog",
                        column: x => x.BlogId,
                        principalSchema: "Property",
                        principalTable: "Blog",
                        principalColumn: "BlogId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BlogId",
                schema: "Property",
                table: "Comment",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment",
                schema: "Property");
        }
    }
}
