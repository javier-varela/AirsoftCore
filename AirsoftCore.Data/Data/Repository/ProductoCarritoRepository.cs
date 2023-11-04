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
    public class ProductoCarritoRepository : Repository<ProductoCarrito>, IProductoCarritoRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductoCarritoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductoCarrito productoCarrito)
        {
            var objFromDB = _db.ProductosCarrito.FirstOrDefault(s => s.Id == productoCarrito.Id);
            objFromDB.Cantidad = productoCarrito.Cantidad;
            
            _db.SaveChanges();
        }
    }
}
