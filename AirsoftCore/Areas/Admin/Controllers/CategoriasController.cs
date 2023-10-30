using AirsoftCore.Data;
using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_contenedorTrabajo.Categoria.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                TempData["SuccesAdd"] = "Categoria Agregada Correctamente";
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            Categoria categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }


            return View(categoria);
        }



        #region llamadas a la API
       
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Categoria objFromDb = await _contenedorTrabajo.Categoria.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { succes = false, message = "Error borrando categoria" });

            }
            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { succes = true, message = "Categoría borrada correctamente" });
        }
        #endregion
        
    }
}
