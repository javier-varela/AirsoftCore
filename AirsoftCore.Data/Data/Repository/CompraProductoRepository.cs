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
    public class CompraProductoRepository : Repository<CompraProducto>, ICompraProductoRepository
    {
        private readonly ApplicationDbContext _db;

        public CompraProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        
    }
}
