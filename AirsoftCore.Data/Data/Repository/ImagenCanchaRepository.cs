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
    public class ImagenCanchaRepository : Repository<ImagenCancha>, IImagenCanchaRepository
    {
        private readonly ApplicationDbContext _db;

        public ImagenCanchaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public  void Update(ImagenCancha imagenCancha)
        {
            var objFromDB = _db.ImagenesCanchas.FirstOrDefault(s => s.Id == imagenCancha.Id);
            objFromDB.Url = imagenCancha.Url;
            
            _db.SaveChanges();
        }
    }
}
