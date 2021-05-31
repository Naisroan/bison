using System;
using System.Collections.Generic;

#nullable disable

namespace bison_api.Model
{
    public partial class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
