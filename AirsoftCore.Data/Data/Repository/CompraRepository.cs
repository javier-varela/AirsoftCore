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
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        private readonly ApplicationDbContext _db;

        public CompraRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        
    }
}
