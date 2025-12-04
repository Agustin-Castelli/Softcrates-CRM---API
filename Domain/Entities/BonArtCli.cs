using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BonArtCli
    {
        public int CodCli { get; set; }
        public string CodArt { get; set; }
        public short CodClaBon { get; set; }
        public bool Inactivo { get; set; }
    }
}
