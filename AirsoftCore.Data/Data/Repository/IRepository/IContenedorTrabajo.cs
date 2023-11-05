﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Data.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        ICategoriaRepository Categoria { get; }

        IUsuarioRepository Usuario { get; }

        IProductoRepository Producto { get; }

        IProductoCarritoRepository ProductoCarrito { get; }

        IImagenProductoRepository ImagenProducto { get; }

        IMonopolyRepository Monopoly { get; }

        void Save();

    }
}
