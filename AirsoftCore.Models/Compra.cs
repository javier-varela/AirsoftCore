using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        public List<CompraProducto> Productos { get; set; }

        [DataType(DataType.Date)]  
        public DateTime FechaCompra { get; set; }
    }
}
