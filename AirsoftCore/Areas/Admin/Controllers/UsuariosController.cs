using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
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

       
    }
}
