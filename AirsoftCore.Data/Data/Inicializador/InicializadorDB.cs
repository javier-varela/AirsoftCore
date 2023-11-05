using AirsoftCore.Models;
using AirsoftCore.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Data.Data.Inicializador
{
    public class InicializadorDB : IInicializadorDB
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InicializadorDB(ApplicationDbContext db, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;


        }

        public  void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }


            if (_db.Roles.Any(rol => rol.Name == Roles.Admin)) return;

            //Creacion de roles
            _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.Usuario)).GetAwaiter().GetResult();

            //Creacion del usuario inicial

            _userManager.CreateAsync(new Usuario
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                Nombre = "admin",

            }, "Admin123#").GetAwaiter().GetResult();

            Usuario usuario = _db.Usuarios
                .Where(us => us.Email == "admin@admin.com")
                .FirstOrDefault();
            _userManager.AddToRoleAsync(usuario, Roles.Admin).GetAwaiter().GetResult();



        }
    }
}
