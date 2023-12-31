﻿using AirsoftCore.Models;
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
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraProducto> DetallesCompra { get; set; }
        public DbSet<ProductoCarrito> ProductosCarrito { get; set; }
        public DbSet<ImagenProducto> ImagenesProducto { get; set; }

        public DbSet<Monopoly> Monopoly { get; set; }

        public DbSet<ImagenCancha> ImagenesCanchas { get; set; }
        public DbSet<FechaCierre> FechasCierre { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Cancha> Canchas { get; set; }

    }

}