using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADevSolvedIt.Data.Migrations
{
    public partial class PostThanksCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThanksCount",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThanksCount",
                table: "Posts");
        }
    }
}
