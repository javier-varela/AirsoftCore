﻿using AirsoftCore.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Data.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
       private readonly ApplicationDbContext _db;

        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            Usuario = new UsuarioRepository(_db);
            Producto = new ProductoRepository(_db);
            ImagenProducto = new ImagenProductoRepository(_db);
        }

        public ICategoriaRepository Categoria {  get; private set; }
        public IProductoRepository Producto { get; private set; }

        public IUsuarioRepository Usuario { get; private set; }

        public IImagenProductoRepository ImagenProducto { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
