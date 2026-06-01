using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class ArticuloDto
    {
        public string CodArt { get; set; }
        public string DesArt { get; set; }
        public decimal Precio { get; set; }
        public int Inactivo { get; set; } // ⬅️ AGREGAR (bool para facilidad de uso en la app)
    }
}
