using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class ProductoCarrito
    {
        [Key]
        public int Id { get; set; }

        public string UsuarioId { get; set; }

        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public ProductoCarrito(string usuarioId, int productoId, int cantidad)
        {
            UsuarioId = usuarioId;
            ProductoId = productoId;
            Cantidad = cantidad;
        }
        public ProductoCarrito()
        {
            
        }

    }
}
