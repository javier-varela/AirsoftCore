using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftCore.Data.Migrations
{
    public partial class MigracionDeConfianza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Canchas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Canchas");
        }
    }
}
