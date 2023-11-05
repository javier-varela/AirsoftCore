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
    public class MonopolyRepository : Repository<Monopoly>, IMonopolyRepository
    {
        private readonly ApplicationDbContext _db;

        public MonopolyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

 

        public void Update(Monopoly monopoly)
        {
            var objFromDB = _db.Monopoly.FirstOrDefault(o => o.Id == monopoly.Id);
            objFromDB.DolarToPoints = monopoly.DolarToPoints;

            _db.SaveChanges();
        }
    }
}
