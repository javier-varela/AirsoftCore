using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class CompraProducto
    {
        [Key]
        public int Id { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
    }
}
