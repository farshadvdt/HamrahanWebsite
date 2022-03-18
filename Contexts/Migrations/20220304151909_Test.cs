using Microsoft.EntityFrameworkCore.Migrations;

namespace HamrahanTemplate.persistence.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "Users",
                newName: "TelePhone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TelePhone",
                table: "Users",
                newName: "Mobile");
        }
    }
}
