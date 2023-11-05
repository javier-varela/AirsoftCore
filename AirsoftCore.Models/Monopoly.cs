using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class Monopoly
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Valor de Moneda( Puntos )")]
        public double DolarToPoints { get; set; }

    }
}
