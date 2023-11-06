using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using AirsoftCore.Models.ViewModels;
using AirsoftCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Client.Controllers
{
    [Authorize(Roles = Roles.Usuario)]
    [Area("Client")]
    public class ComprasController : Controller
    {


        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly UserManager<Usuario> _userManager;
        public ComprasController(IContenedorTrabajo contenedorTrabajo, UserManager<Usuario> userManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string idUser = _userManager.GetUserId(User);
            return View(_contenedorTrabajo.Compra.GetAll(c=>c.UsuarioId == idUser,includeProperties: "Productos"));
        }

        [HttpGet]

        public IActionResult Details(int id)
        {

            DetailsCompraProductoViewModel model = new()
            {
                Compra = _contenedorTrabajo.Compra.Get(id),
                ProductosCompra = _contenedorTrabajo.CompraProducto.GetAll(cp => cp.CompraId == id, includeProperties: "Producto")
            };
            return View(model);
        }

    }
}
