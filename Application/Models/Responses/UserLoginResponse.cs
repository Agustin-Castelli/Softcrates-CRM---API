using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class UserLoginResponse
    {
        public string CodUsr { get; set; }
        public string Name { get; set; }
        public string IsAdmin { get; set; }
        public string Token { get; set; }  // En el futuro vendrá el JWT acá

        public UserLoginResponse(string codUsr, string name, string isAdmin, string token)
        {
            CodUsr = codUsr;
            Name = name;
            IsAdmin = isAdmin;
            Token = token;
        }
    }
}
