using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bison_api.DTO
{
    public class UsuarioTokenDTO
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
