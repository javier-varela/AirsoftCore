using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftCore.Data.Migrations
{
    public partial class ActualizacionModeloMonopoly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DolarToPoints",
                table: "Monopoly",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DolarToPoints",
                table: "Monopoly",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
