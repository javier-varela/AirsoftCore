using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using AirsoftCore.Utilities;
using AirsoftCore.Models.ViewModels;

namespace AirsoftCore.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = Roles.Usuario)]
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
            var productosCarrito = _contenedorTrabajo.ProductoCarrito.GetAll((p) => p.UsuarioId == userId, includeProperties: "Producto");
            var user = _contenedorTrabajo.Usuario.GetFirstOrDefault(u => u.Id == userId);
            double total = 0;
            var _ProductosCarritoVM = new List<ProductoCarritoViewModel>();

            foreach (var productoCarrito in productosCarrito)
            {
                double _totalProd = productoCarrito.Cantidad * productoCarrito.Producto.Precio;

                _ProductosCarritoVM.Add(new ProductoCarritoViewModel()
                {
                    ProductoCarrito = productoCarrito,
                    Total = _totalProd
                });

                total += _totalProd;
            }

            var model = new ClientCarritoViewModel()
            {
                ProductosCarritoVM = _ProductosCarritoVM as IEnumerable<ProductoCarritoViewModel>,
                Total = total,
                PuntosUsuario =user.Puntos
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var producto = _contenedorTrabajo.Producto.Get(id);

            var _producto = _contenedorTrabajo.ProductoCarrito.GetFirstOrDefault(p => p.ProductoId == id);


            if (_producto != null)
            {
                int _id = _producto.Id;
                return Redirect("/Client/Carrito/Edit/" + _id);
            }
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
            var _producto = _contenedorTrabajo.Producto.Get(productoCarrito.ProductoId);
            if (_producto.Stock < productoCarrito.Cantidad)
            {
                productoCarrito.Cantidad = productToInsert.Cantidad;
                productoCarrito.Producto = _producto;
                TempData["Error"] = "Error Agregando Producto Stock = " + _producto.Stock;
                return View("Add", productoCarrito);
            }
            _contenedorTrabajo.ProductoCarrito.Add(productToInsert);
            _contenedorTrabajo.Save();
            TempData["SuccesAdd"] = "Producto Agregada Correctamente";
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var ProductoCarrito = _contenedorTrabajo.ProductoCarrito.GetFirstOrDefault(p => p.Id == id, includeProperties: "Producto");
            return View(ProductoCarrito);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductoCarrito productoCarrito)
        {
            var _producto = _contenedorTrabajo.Producto.Get(productoCarrito.ProductoId);
            if (_producto.Stock < productoCarrito.Cantidad)
            {
                TempData["Error"] = "Error Actualizando el Producto | Stock = " + _producto.Stock;
                return RedirectToAction(nameof(Edit));
            }
            _contenedorTrabajo.ProductoCarrito.Update(productoCarrito);
            _contenedorTrabajo.Save();
            TempData["SuccesAdd"] = "Producto Actualizado Correctamente";
            return RedirectToAction(nameof(Index));
        }


        #region llamadas a la API

        [HttpPost]
        public async Task<IActionResult> Pagar()
        {
            string userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);

            var productosCarrito = (List<ProductoCarrito>)_contenedorTrabajo.ProductoCarrito.GetAll((p) => p.UsuarioId == userId, includeProperties: "Producto");
            double total = 0;

            if (productosCarrito.Count == 0)
            {
                return Json(new { succes = false, message = "No tienes Productos en el Carrito" });

            }


            productosCarrito.ForEach(product =>
            {
                double precio = product.Producto.Precio;
                total += precio * product.Cantidad;

            });

            if (total > user.Puntos)
            {
                return Json(new { succes = false, message = "No tienes suficientes puntos en tu cuenta" });
            }
            List<CompraProducto> productosComprados = new();

            foreach (var productCarrito in productosCarrito)
            {
                if (productCarrito.Producto.Stock < productCarrito.Cantidad)
                {
                    _contenedorTrabajo.ProductoCarrito.Remove(productCarrito);
                    return Json(new { succes = false, message = "No existen suficientes productos en Stock de " + productCarrito.Producto.Nombre });
                }
                productosComprados.Add(new CompraProducto()
                {
                    Producto = productCarrito.Producto,
                    Cantidad = productCarrito.Cantidad,

                });

                _contenedorTrabajo.ProductoCarrito.Remove(productCarrito);
                productCarrito.Producto.Stock -= productCarrito.Cantidad;
                _contenedorTrabajo.Producto.Update(productCarrito.Producto);
            }

            _contenedorTrabajo.Compra.Add(new Compra()
            {
                UsuarioId = userId,
                FechaCompra = DateTime.Now,
                Productos = productosComprados
            });


            _contenedorTrabajo.Usuario.UpdatePuntos(user.Puntos - total, userId);

            _contenedorTrabajo.Save();


            return Json(new { succes = true, message = "Pagado: " + total + "Te quedan: " + user.Puntos });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = await _contenedorTrabajo.ProductoCarrito.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { succes = false, message = "Error borrando articulo del carrito" });

            }
            _contenedorTrabajo.ProductoCarrito.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { succes = true, message = "Articulo borrado del carrito correctamente" });
        }

        #endregion





    }
}
