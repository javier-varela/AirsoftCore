using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models.ViewModels;
using AirsoftCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Area("Admin")]
    public class ComprasController : Controller
    {


        private readonly IContenedorTrabajo _contenedorTrabajo;
        public ComprasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View(_contenedorTrabajo.Compra.GetAll(includeProperties: "Productos"));
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
