using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using AirsoftCore.Models.ViewModels;
using AirsoftCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = Roles.Usuario)]
    public class ReservasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly UserManager<Usuario> _userManager;

        public ReservasController(IContenedorTrabajo contenedorTrabajo, UserManager<Usuario> userManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            return View(_contenedorTrabajo.Reserva.GetAll(r => r.UsuarioId == userId));
        }

        [HttpGet]
        public IActionResult Create(int id)
        {

            var model = new CreateReservaViewModel() { 
                Cancha = _contenedorTrabajo.Cancha.Get(id),
                Reserva = new Reserva()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Create(CreateReservaViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Reserva.UsuarioId = _userManager.GetUserId(User);
                model.Reserva.CanchaId = model.Cancha.Id;
                model.Reserva.Precio = model.Reserva.DuracionHoras * model.Cancha.PrecioHora;
                _contenedorTrabajo.Reserva.Add(model.Reserva);
                _contenedorTrabajo.Save();
                return View(nameof(Index));
            }
            
            
            return View(model);
        }
    }
}
