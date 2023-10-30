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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categorias.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }
            );
        }

        public void Update(Categoria categoria)
        {
            var objFromDB = _db.Categorias.FirstOrDefault(s => s.Id == categoria.Id);
            objFromDB.Name = categoria.Name;
            objFromDB.Orden = categoria.Orden;

            _db.SaveChanges();
        }
    }
}
