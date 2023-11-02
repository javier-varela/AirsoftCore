using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ProductosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Producto.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductoViewModel model = new()
            {
                Producto = new AirsoftCore.Models.Producto(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            return View(model);
        }
    }
}
