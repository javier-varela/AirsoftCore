using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftCore.Data.Migrations
{
    public partial class ActualizacionModelosUsuarioCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompraProducto_AspNetUsers_UsuarioId",
                table: "CompraProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_CompraProducto_Productos_ProductoId",
                table: "CompraProducto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraProducto",
                table: "CompraProducto");

            migrationBuilder.DropIndex(
                name: "IX_CompraProducto_UsuarioId",
                table: "CompraProducto");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CompraProducto");

            migrationBuilder.RenameTable(
                name: "CompraProducto",
                newName: "DetallesCompra");

            migrationBuilder.RenameIndex(
                name: "IX_CompraProducto_ProductoId",
                table: "DetallesCompra",
                newName: "IX_DetallesCompra_ProductoId");

            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "DetallesCompra",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesCompra",
                table: "DetallesCompra",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_CompraId",
                table: "DetallesCompra",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_Compras_CompraId",
                table: "DetallesCompra",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_Productos_ProductoId",
                table: "DetallesCompra",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_Compras_CompraId",
                table: "DetallesCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_Productos_ProductoId",
                table: "DetallesCompra");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesCompra",
                table: "DetallesCompra");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCompra_CompraId",
                table: "DetallesCompra");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "DetallesCompra");

            migrationBuilder.RenameTable(
                name: "DetallesCompra",
                newName: "CompraProducto");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesCompra_ProductoId",
                table: "CompraProducto",
                newName: "IX_CompraProducto_ProductoId");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "CompraProducto",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraProducto",
                table: "CompraProducto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompraProducto_UsuarioId",
                table: "CompraProducto",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraProducto_AspNetUsers_UsuarioId",
                table: "CompraProducto",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraProducto_Productos_ProductoId",
                table: "CompraProducto",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }
    }
}
