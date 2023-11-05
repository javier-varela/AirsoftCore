using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirsoftCore.Data.Migrations
{
    public partial class ActualizacionModeloUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompraProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraProducto_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompraProducto_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraProducto_ProductoId",
                table: "CompraProducto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraProducto_UsuarioId",
                table: "CompraProducto",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraProducto");
        }
    }
}
