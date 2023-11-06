using AirsoftCore.Data.Data.Repository.IRepository;
using AirsoftCore.Models;
using AirsoftCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Area("Admin")]
    public class PointsController : Controller
    {
  
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public PointsController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult Edit()
        {
            if(_contenedorTrabajo.Monopoly.GetFirstOrDefault() == null)
            {
                _contenedorTrabajo.Monopoly.Add(new Monopoly()
                {
                    DolarToPoints = (double)1.1,
                });
                _contenedorTrabajo.Save();
            }
            return View(_contenedorTrabajo.Monopoly.GetFirstOrDefault());
        }
        [HttpPost]
        public IActionResult Edit(Monopoly monopoly)
        {
            _contenedorTrabajo.Monopoly.Update(monopoly);
            _contenedorTrabajo.Save();
            return View();
        }
    }
}
