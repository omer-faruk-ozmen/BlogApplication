using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApplication.Infrastructure.Persistence.Migrations
{
    public partial class tagManytoManyTest1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tags",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Tags",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
