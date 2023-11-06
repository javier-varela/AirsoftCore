using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class Cancha
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        public List<ImagenCancha> Imagenes { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public List<FechaCierre> FechasCierre { get; set; }

        public List<Reserva> Reservas { get; set; }

        public double PrecioHora { get; set; }

        public Cancha()
        {
            
        }


    }
}
