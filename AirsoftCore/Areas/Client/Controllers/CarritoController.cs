using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;


namespace AirsoftCore.Areas.Client.Controllers
{
    [Area("Client")]
    public class CarritoController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly UserManager<Usuario> _userManager;

        public CarritoController(IContenedorTrabajo contenedorTrabajo, UserManager<Usuario> userManager)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            return View(_contenedorTrabajo.ProductoCarrito.GetAll((p) => p.UsuarioId == userId, includeProperties: "Producto"));
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var producto = _contenedorTrabajo.Producto.Get(id);

            ProductoCarrito pCarrito = new()
            {
                ProductoId = producto.Id,
                Producto = producto,
            };


            return View(pCarrito);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductoCarrito productoCarrito)
        {

            string userId = _userManager.GetUserId(User);
            ProductoCarrito productToInsert = new(userId, productoCarrito.ProductoId, productoCarrito.Cantidad);
            _contenedorTrabajo.ProductoCarrito.Add(productToInsert);
            _contenedorTrabajo.Save();
            TempData["SuccesAdd"] = "Producto Agregada Correctamente";
            return RedirectToAction(nameof(Index));

        }


        #region llamadas a la API

        [HttpPost]
        public async Task<IActionResult> Pagar()
        {
            string userId = _userManager.GetUserId(User);
            var productos = (List<ProductoCarrito>)_contenedorTrabajo.ProductoCarrito.GetAll((p) => p.UsuarioId == userId, includeProperties: "Producto");
            double total = 0;
            if (productos.Count == 0)
            {
                return Json(new { succes = false, message = "No tienes Productos en el Carrito" });

            }

            
            foreach (var product in productos)
            {
                double precio = product.Producto.Precio;
                total += precio*product.Cantidad;

            }
           
            return Json(new { succes = false, message = "Pagado: " + total });
            _contenedorTrabajo.Save();
        }
        #endregion

      



    }
}
