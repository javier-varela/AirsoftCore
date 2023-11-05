using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models.ViewModels
{
    public class ClientCarritoViewModel
    {
        public IEnumerable<ProductoCarritoViewModel> ProductosCarritoVM {  get; set; }

        public double Total {  get; set; }

        public double PuntosUsuario { get; set; }

   }
}
