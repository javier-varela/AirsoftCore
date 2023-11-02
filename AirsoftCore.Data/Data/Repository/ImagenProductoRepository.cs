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
    public class ImagenProductoRepository : Repository<ImagenProducto>, IImagenProductoRepository
    {
        private readonly ApplicationDbContext _db;

        public ImagenProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ImagenProducto imagenProducto)
        {
            var objFromDB = _db.ImagenesProducto.FirstOrDefault(s => s.Id == imagenProducto.Id);
            objFromDB.Url = imagenProducto.Url;

            _db.SaveChanges();
        }
    }
}
