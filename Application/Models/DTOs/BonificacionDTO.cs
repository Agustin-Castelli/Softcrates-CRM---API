using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class BonificacionDTO
    {
        public short CodClaBon { get; set; }
        public decimal PorcentajeBonificacion { get; set; }
        public string TipoEscala { get; set; } = string.Empty; // "I" = Importe, "C" = Cantidad
    }

    public class ArticuloConBonificacionDTO
    {
        public string CodArt { get; set; } = string.Empty;
        public string DesArt { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public short? CodClaBon { get; set; } // null si no tiene bonificación
        public decimal? PorcentajeBonificacion { get; set; } // Porcentaje de la primera escala
        public bool TieneBonificacion => CodClaBon.HasValue && PorcentajeBonificacion.HasValue;
    }
}
