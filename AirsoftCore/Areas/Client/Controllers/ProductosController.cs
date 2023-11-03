using AirsoftCore.Data.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Client.Controllers
{
    [Area("Client")]
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

        
       
    }
}
