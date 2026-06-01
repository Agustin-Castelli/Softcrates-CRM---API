using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    // TABLA DE PRODUCTOS
    public class Artic
    {
        public string CodArt { get; set; } = string.Empty;
        public string DesArt { get; set; } = string.Empty;
        public byte Inactivo { get; set; } = 0;

        public ICollection<PreVen>? Precios { get; set; }
        public ICollection<PedWebArt>? PedidosDetalle { get; set; }
    }
}
