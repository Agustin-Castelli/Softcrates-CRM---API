using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BonClaDet
    {
        public short CodClaBon { get; set; }
        public short Secuencia { get; set; }
        public char? TipEsc { get; set; }
        public decimal? ValEscDes { get; set; }
        public decimal? ValEscHas { get; set; }
        public decimal? PorBonImp { get; set; }
        public decimal? PorBonCan { get; set; }
        public char? DisFac { get; set; }
    }
}
