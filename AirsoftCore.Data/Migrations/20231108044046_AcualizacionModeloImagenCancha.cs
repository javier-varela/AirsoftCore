using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftCore.Data.Migrations
{
    public partial class AcualizacionModeloImagenCancha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_Compras_CompraId",
                table: "DetallesCompra");

            migrationBuilder.AlterColumn<int>(
                name: "CompraId",
                table: "DetallesCompra",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_Compras_CompraId",
                table: "DetallesCompra",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_Compras_CompraId",
                table: "DetallesCompra");

            migrationBuilder.AlterColumn<int>(
                name: "CompraId",
                table: "DetallesCompra",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_Compras_CompraId",
                table: "DetallesCompra",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");
        }
    }
}
