using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADevSolvedIt.Data.Migrations
{
    public partial class ViewLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViewLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCrawlerSimple = table.Column<bool>(type: "bit", nullable: false),
                    isCrawlerAdvanced = table.Column<bool>(type: "bit", nullable: false),
                    PostSlug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewLogs");
        }
    }
}
