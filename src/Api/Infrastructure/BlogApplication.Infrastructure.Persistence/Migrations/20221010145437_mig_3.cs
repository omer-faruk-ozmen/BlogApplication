using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApplication.Infrastructure.Persistence.Migrations
{
    public partial class mig_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Categories",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
