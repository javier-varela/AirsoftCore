using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models.ViewModels;
using AirsoftCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using AirsoftCore.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace AirsoftCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Area("Admin")]
    public class CanchasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CanchasController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _webHostEnvironment = webHostEnvironment;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Cancha.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cancha cancha)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _webHostEnvironment.WebRootPath;
                IFormFileCollection imagenes = HttpContext.Request.Form.Files;

                if (cancha.Id == 0)
                {
                    // Crea una lista para almacenar las URL de las imágenes
                    List<ImagenCancha> urlsImagenes = new();

                    // Guarda cada imagen en la carpeta "@imagenes\articulos"
                    foreach (var imagen in imagenes)
                    {

                        string nombreImagen = Guid.NewGuid().ToString();
                        var subidas = Path.Combine(rutaPrincipal, @"imagenes\canchas");
                        var extension = Path.GetExtension(imagen.FileName);

                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreImagen + extension), FileMode.Create))
                        {
                            imagen.CopyTo(fileStreams);
                        }

                        // Agrega la URL de la imagen a la lista
                        urlsImagenes.Add(new ImagenCancha() { Url = @"\imagenes\canchas\" + nombreImagen + extension });
                    }

                    // Guarda las URL de las imágenes en la propiedad `URLS` del producto
                    cancha.Imagenes = urlsImagenes;

                    _contenedorTrabajo.Cancha.Add(cancha);
                    _contenedorTrabajo.Save();

                    TempData["Succes"] = "Cancha agregado correctamente";

                    return RedirectToAction(nameof(Index));
                }
            }


            return View(cancha);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_contenedorTrabajo.Cancha.Get(id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cancha cancha)
        {
            _contenedorTrabajo.Cancha.Update(cancha);
            _contenedorTrabajo.Save();
           return RedirectToAction(nameof(Index));

        }

        #region llamadas a la API

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Cancha.GetFirstOrDefault((c) => c.Id == id, includeProperties: "Imagenes");
            if (objFromDb == null)
            {
                return Json(new { succes = false, message = "Error borrando cancha" });

            }
            _contenedorTrabajo.Cancha.Remove(objFromDb);
            if (objFromDb.Imagenes != null)
            {
                var rutaDirectorioPrincipal = _webHostEnvironment.WebRootPath;
                foreach (var imagen in objFromDb.Imagenes)
                {
                    _contenedorTrabajo.ImagenCancha.Remove(imagen);
                    var rutaImagen = Path.Combine(rutaDirectorioPrincipal, imagen.Url.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                }
            }

          
            _contenedorTrabajo.Save();
            return Json(new { succes = true, message = "Cancha borrada correctamente" });
        }
        #endregion
    }
}
