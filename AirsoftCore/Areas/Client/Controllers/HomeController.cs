using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AirsoftCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly UserManager<Usuario> _userManager;

        public HomeController(IContenedorTrabajo contenedorTrabajo, UserManager<Usuario> userManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Producto.GetAll(includeProperties: "Imagenes,Categoria"));
        }

        [HttpGet]
        public IActionResult Product(int id)
        {

            return View(_contenedorTrabajo.Producto.GetFirstOrDefault(p => p.Id == id, includeProperties: "Imagenes,Categoria"));
        }
        [HttpGet]
        public IActionResult Cancha(int id)
        {

            return View(_contenedorTrabajo.Cancha.GetFirstOrDefault(c => c.Id == id, includeProperties: "Imagenes"));
        }

        [HttpGet]
        public IActionResult Canchas()
        {

            return View(_contenedorTrabajo.Cancha.GetAll(includeProperties: "Imagenes,FechasCierre"));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Puntos()
        {
            string userId = _userManager.GetUserId(User);
            var user = _contenedorTrabajo.Usuario.GetFirstOrDefault(u=>u.Id==userId);

            if (user != null)
            {
                return Json(new { puntos = user.Puntos.ToString() });
            }

            return Json(new { puntos = "0" });

        }
    }
}