using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AirsoftCore.Data.Data.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public  void Update(Producto producto)
        {
            var objFromDB = _db.Productos.FirstOrDefault(s => s.Id == producto.Id);
            objFromDB.Nombre = producto.Nombre;
            objFromDB.Descripcion = producto.Descripcion;
            objFromDB.Categoria = producto.Categoria;
            objFromDB.CategoriaId = producto.CategoriaId;
            objFromDB.Stock = producto.Stock;
            objFromDB.Precio = producto.Precio;
            
            _db.SaveChanges();
        }
    }
}
