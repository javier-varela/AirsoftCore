using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class FechaCierre
    {
        [Key] 
        public int Id { get; set; }

        [DataType(DataType.Time)]
        public DateTime Fecha {  get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
