using AirsoftCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirsoftCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoCarrito> ProductosCarrito { get; set; }
        public DbSet<ImagenProducto> ImagenesProducto { get; set; }

        public DbSet<Monopoly> Monopoly { get; set; }

    }

}