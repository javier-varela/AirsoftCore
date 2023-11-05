using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public void UpdateCompras(List<Compra> compras, string id)
        {
            var usuarioFromDb = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            usuarioFromDb.Compras = compras;
            _db.SaveChanges();
        }

        public void UpdatePuntos(double points, string id)
        {
            var usuarioFromDb = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            usuarioFromDb.Puntos = points;
            _db.SaveChanges();
        }
    }
}
