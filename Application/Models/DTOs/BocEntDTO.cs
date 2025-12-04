using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class BocEntDTO
    {
        public int CodCli { get; set; }
        public int NroBocEnt { get; set; }
        public string NomBocEnt { get; set; } = string.Empty;
        public string? DomBocEnt { get; set; }
    }
}
