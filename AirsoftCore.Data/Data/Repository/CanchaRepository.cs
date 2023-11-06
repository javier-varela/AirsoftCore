using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirsoftCore.Data.Data.Repository
{
    public class CanchaRepository : Repository<Cancha>, ICanchaRepository
    {
        private readonly ApplicationDbContext _db;

        public CanchaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Cancha cancha)
        {
            var objFromDB = _db.Canchas.FirstOrDefault(c => c.Id == cancha.Id);
            objFromDB.Nombre = cancha.Nombre;
            objFromDB.Descripcion = cancha.Descripcion;
            objFromDB.PrecioHora = cancha.PrecioHora;

            _db.SaveChanges();
        }
    }
}
