using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Factura
    {
        public string CSIDcbtDeu { get; set; } = string.Empty;
        public DateTime FechaVto { get; set; }
        public int CodCli { get; set; }
        public string NroDocCli { get; set; } = string.Empty;
        public short CodTipoCbt { get; set; }
        public short CemCbt { get; set; }
        public int NroCbt { get; set; }
        public DateTime FechaCbt { get; set; }
        public string CodCta { get; set; } = string.Empty;
        public short CodMon { get; set; }
        public decimal? ImpCot { get; set; }
        public decimal ImporteOriginal { get; set; }
        public decimal Saldo { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public string? CodVen { get; set; }
        public string? CodZonaVta { get; set; }
        public decimal ImpIntCob { get; set; }
        public string CobInt { get; set; } = string.Empty;
        public string DifCot { get; set; } = string.Empty;
        public DateTime? FechaCbtOri { get; set; }
        public string? CSIDci { get; set; }
        public string? IntMora { get; set; }
        public decimal? ImpIntDeu { get; set; }
        public short? NroCuo { get; set; }
        public string? CSIDndDev { get; set; }
        public decimal? ImpIf { get; set; }
        public decimal? IvaIf { get; set; }
        public DateTime? FechaAnuPorCi { get; set; }
        public string? DtoRec { get; set; }
        public string? CodSuc { get; set; }
        public string? CodDimEle1 { get; set; }
        public DateTime? FechaCasFlo { get; set; }
        public string? ExcDeuFrgRec { get; set; }
        public string ClaveEmp { get; set; } = string.Empty;
        public string? EstadoCC { get; set; }
    }
}
