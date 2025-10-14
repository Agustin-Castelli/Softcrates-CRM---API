using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client
    {
        public int CodCli { get; set; }
        public string NomCli { get; set; } = string.Empty;
        public decimal SaldoDeuCc { get; set; }
        public decimal SaldoVencCc { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal LimiteCreditoUso { get; set; }
    }
}
