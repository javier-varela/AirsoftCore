using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using AirsoftCore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Client.Controllers
{
    [Area("Client")]
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
        public IActionResult Create(Reserva reserva)
        {
            return View(reserva);
        }
    }
}
