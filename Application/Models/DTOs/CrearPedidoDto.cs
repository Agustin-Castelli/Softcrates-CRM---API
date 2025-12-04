using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class CrearPedidoDto
    {
        public short CodTipCbt { get; set; }                   // Tipo de comprobante
        public short CemCbt { get; set; }                      // Centro emisor
        public int NroCbt { get; set; }                      // Número comprobante
        public int CodCli { get; set; }                      // Cliente
        public int Confirmado { get; set; } = 0;             // Confirmacion del pedido (0: no / 1: si)
        public int NroBocEnt { get; set; }

        public List<CrearPedidoDetalleDto> Detalles { get; set; } = new();
    }

    public class CrearPedidoDetalleDto
    {
        public string CodArt { get; set; } = string.Empty;
        public string DesArt { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

}
