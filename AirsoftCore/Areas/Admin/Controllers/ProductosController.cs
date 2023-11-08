using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using AirsoftCore.Models.ViewModels;
using AirsoftCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Producto.GetAll(includeProperties:"Categoria"));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _webHostEnvironment.WebRootPath;
                IFormFileCollection imagenes = HttpContext.Request.Form.Files;

                if (model.Producto.Id == 0)
                {
                    // Crea una lista para almacenar las URL de las imágenes
                    List<ImagenProducto> urlsImagenes = new List<ImagenProducto>();

                    // Guarda cada imagen en la carpeta "@imagenes\articulos"
                    foreach (var imagen in imagenes)
                    {

                        string nombreImagen = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(rutaPrincipal, @"imagenes\productos");
                        var extension = Path.GetExtension(imagen.FileName);

                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreImagen + extension), FileMode.Create))
                        {
                            imagen.CopyTo(fileStreams);
                        }

                        // Agrega la URL de la imagen a la lista
                        urlsImagenes.Add(new ImagenProducto() {Url = @"\imagenes\productos\" + nombreImagen + extension });
                    }

                    // Guarda las URL de las imágenes en la propiedad `URLS` del producto
                    model.Producto.Imagenes = urlsImagenes;

                    _contenedorTrabajo.Producto.Add(model.Producto);
                    _contenedorTrabajo.Save();
                    TempData["Succes"] = "Producto agregado correctamente";
                    return RedirectToAction(nameof(Index));
                }
            }

            model.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProductoViewModel model = new()
            {
                Producto = new AirsoftCore.Models.Producto(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            
            model.Producto = _contenedorTrabajo.Producto.Get(id);
            if (model.Producto == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductoViewModel model)
        {
            if (ModelState.IsValid)
            {

                _contenedorTrabajo.Producto.Update(model.Producto);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            model.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(model.Producto.Id);
        }

        #region llamadas a la API

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb =  _contenedorTrabajo.Producto.GetFirstOrDefault((product)=> product.Id==id, includeProperties: "Imagenes,Categoria");
            if (objFromDb == null)
            {
                return Json(new { succes = false, message = "Error borrando categoria" });

            }
            _contenedorTrabajo.Producto.Remove(objFromDb);
            if (objFromDb.Imagenes != null) {
                var rutaDirectorioPrincipal = _webHostEnvironment.WebRootPath;
                foreach (var imagen in objFromDb.Imagenes)
                {
                    _contenedorTrabajo.ImagenProducto.Remove(imagen);
                    var rutaImagen = Path.Combine(rutaDirectorioPrincipal, imagen.Url.TrimStart('\\'));
                    if(System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }
                    
                }
            }

         
            _contenedorTrabajo.Save();
            return Json(new { succes = true, message = "Categoría borrada correctamente" });
        }
        #endregion
    }
}
