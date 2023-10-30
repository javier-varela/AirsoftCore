using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Data.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BloquearUsuario(string id)
        {
            var usuarioFromDb = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            usuarioFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            _db.SaveChanges();
        }

        public void DesbloquearUsuario(string id)
        {
            var usuarioFromDb = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            usuarioFromDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
