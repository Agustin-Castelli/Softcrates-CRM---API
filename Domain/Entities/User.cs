using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public string CodUsr { get; set; } = string.Empty;
        public string? NomUsr { get; set; }
        public string? AdmUsr { get; set; }
        public string? PwdUsr { get; set; }
        public bool Inactivo { get; set; }
        public string CodGrp { get; set; } = string.Empty;
    }
}
