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
    public class ReservaRepository : Repository<Reserva>, IReservaRepository
    {
        private readonly ApplicationDbContext _db;

        public ReservaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public  void Update(Reserva reserva)
        {
            var objFromDB = _db.Reservas.FirstOrDefault(s => s.Id == reserva.Id);
            objFromDB.FechaHora = reserva.FechaHora;
            objFromDB.DuracionHoras = reserva.DuracionHoras; 
            objFromDB.Precio = reserva.Precio;
            objFromDB.CanchaId = reserva.CanchaId;

            _db.SaveChanges();
        }
    }
}
