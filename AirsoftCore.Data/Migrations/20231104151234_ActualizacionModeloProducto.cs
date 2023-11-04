using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftCore.Data.Migrations
{
    public partial class ActualizacionModeloProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "Productos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Productos");
        }
    }
}
