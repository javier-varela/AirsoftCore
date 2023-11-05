using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly UserManager<Usuario> _userManager;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo, UserManager<Usuario> userManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
              _userManager = userManager;
    }

        [HttpGet]
        public IActionResult Index()
        {
            //Opcion 1: Obtener Todos Los Usuarios
            return View(_contenedorTrabajo.Usuario.GetAll());
        }


        [HttpPost]
        public IActionResult Bloquear(string id)
        {
            try
            {
                _contenedorTrabajo.Usuario.BloquearUsuario(id);
                _contenedorTrabajo.Save();
            }catch (Exception) {
                return Json(new { succes = false, message = "No se ha podido Bloquear al Usuario" });
            }
            
            return Json(new { succes = true, message = "Usuario Bloqueado correctamente" });
        }

        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Usuario.DesbloquearUsuario(id);
            _contenedorTrabajo.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View(_contenedorTrabajo.Usuario.GetFirstOrDefault((user)=> user.Id == id));
        }

        [HttpPost]
        public IActionResult Edit(Usuario usuario)
        {
            string id = _userManager.GetUserId(User);
            _contenedorTrabajo.Usuario.UpdatePuntos(usuario.Puntos,id);
            return View(usuario);
        }


    }
}
