using Microsoft.EntityFrameworkCore.Migrations;

namespace AKS.Infrastructure.Migrations
{
    public partial class Automapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopicContent",
                table: "Topic",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Topic",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Topic",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Topic",
                newName: "TopicContent");
        }
    }
}
