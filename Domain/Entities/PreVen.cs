using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    // TABLA DE PRECIOS
    public class PreVen
    {
        public short CodList { get; set; }
        public string CodArt { get; set; } = string.Empty;
        public string CodUniMed { get; set; } = string.Empty;
        public short? CodMon { get; set; }
        public decimal? Precio { get; set; }
        public DateTime? FecUltMod { get; set; }
        public DateTime? FecIng { get; set; }
        public string? UsrIng { get; set; }
        public string? WksIng { get; set; }
        public DateTime? FecExp { get; set; }

        public Artic? Articulo { get; set; }
    }

}
