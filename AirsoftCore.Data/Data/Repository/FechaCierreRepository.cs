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
    public class FechaCierreRepository : Repository<FechaCierre>, IFechaCierreRepository
    {
        private readonly ApplicationDbContext _db;

        public FechaCierreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public  void Update(FechaCierre fechaCierre)
        {
            var objFromDB = _db.FechasCierre.FirstOrDefault(s => s.Id == fechaCierre.Id);
            objFromDB.Fecha = fechaCierre.Fecha;
            
            _db.SaveChanges();
        }
    }
}
