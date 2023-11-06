using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class Usuario : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name ="Nombre de Usuario")]
        public string Nombre { get; set; }

        public double Puntos { get; set; }

        public List<ProductoCarrito> ProductosCarrito { get; set; }

        public List<Compra> Compras { get; set;}

        public List<Reserva> Reservas { get; set; }

    }
}
