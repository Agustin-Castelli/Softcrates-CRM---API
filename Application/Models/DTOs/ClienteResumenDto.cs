using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class ClienteResumenDto
    {
        // Datos de cliente
        public int CodCli { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal SaldoActual { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoVencido { get; set; }
        public decimal PorcentajeCreditoUsado { get; set; }

        // Facturas
        public List<FacturaDto> Facturas { get; set; } = new();
    }

    public class FacturaDto
    {
        public string NumeroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}
