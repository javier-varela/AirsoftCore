using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models.ViewModels
{
    public class DetailsCompraProductoViewModel
    {
        public Compra Compra { get; set; }

        public IEnumerable<CompraProducto> ProductosCompra { get; set; }


    }
}
