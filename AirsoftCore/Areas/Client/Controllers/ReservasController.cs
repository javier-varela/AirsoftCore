using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using AirsoftCore.Models.ViewModels;
using AirsoftCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

            var model = new CreateReservaViewModel()
            {
                Cancha = _contenedorTrabajo.Cancha.Get(id),
                Reserva = new Reserva()
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateReservaViewModel model)
        {
            var reserva = model.Reserva;
            double precio = reserva.DuracionHoras * model.Cancha.PrecioHora;

            string userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);

            IEnumerable<Reserva> reservas = _contenedorTrabajo.Reserva.GetAll(c => c.Id == model.Cancha.Id);
            
            if(!reservas.Any())
            {
                foreach (var r in reservas)
                {
                    var horaInicio = r.FechaHora;
                    var horaTermino = horaInicio.AddHours(r.DuracionHoras);

                    if (reserva.FechaHora >= horaInicio && reserva.FechaHora <= horaTermino)
                    {
                        TempData["Error"] = "Ya existe una reservacion: De " + horaInicio.ToString() + " A " + horaTermino.ToString();

                        return View(nameof(Index));
                    }

                    if (precio > user.Puntos)
                    {
                        TempData["Error"] = "No tienes suficientes Puntos, Tienes: " + user.Puntos.ToString();

                        return View(nameof(Index));
                    }
                }
            }
           

            reserva.UsuarioId = userId;
            reserva.CanchaId = model.Cancha.Id;
            reserva.Precio = precio;


            _contenedorTrabajo.Reserva.Add(model.Reserva);
            _contenedorTrabajo.Save();
            TempData["Succes"] = "Reserva Agregada";

            return RedirectToAction("Index");



            //return View(model);
        }

        #region llamadas a la API

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Reserva.Get(id);
            if (objFromDb == null)
            {
                return Json(new { succes = false, message = "Error borrando categoria" });

            }
            _contenedorTrabajo.Reserva.Remove(objFromDb);



            _contenedorTrabajo.Save();
            return Json(new { succes = true, message = "reserva borrada correctamente" });
        }
        #endregion
    }
}
