using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftCore.Models
{
    public class ImagenCancha
    {
        public int Id { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }
    }
}
