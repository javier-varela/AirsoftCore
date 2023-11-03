
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirsoftCore.Models.ViewModels
{
    public class ProductosViewModel
    {
        public IEnumerable<SelectListItem> ListaProductos { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }


    }
}
