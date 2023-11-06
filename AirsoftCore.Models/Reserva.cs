using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        public string UsuarioId { get; set; }

        [Required]
        public int CanchaId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaHora { get; set; }

        [Required]
        public int DuracionHoras { get; set; }

        public double Precio { get; set; }
    }
}
