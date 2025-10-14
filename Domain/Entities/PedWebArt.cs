using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //TABLA DETALLE DE PEDIDO
    public class PedWebArt
    {
        public string Csid { get; set; } = string.Empty;
        public short Secuencia { get; set; }
        public string CodArt { get; set; } = string.Empty;
        public short CodIvaArt { get; set; }
        public short CodClaBon { get; set; }
        public string DesArtAmp { get; set; } = string.Empty;
        public DateTime FecEntArt { get; set; }
        public decimal CanArt { get; set; }
        public decimal PreArt { get; set; }
        public decimal ImpBonArt { get; set; }
        public decimal ImpGraArt { get; set; }
        public decimal ImpDesArt { get; set; }
        public decimal ImpPrecArt { get; set; }
        public decimal ImpNetGraArt { get; set; }
        public decimal ImpIvaArt { get; set; }

        [JsonIgnore]  // Para evitar el loop
        public PedWebCab? Pedido { get; set; }
        public Artic? Articulo { get; set; }
    }
}
