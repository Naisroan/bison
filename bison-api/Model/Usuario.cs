using System;
using System.Collections.Generic;

#nullable disable

namespace bison_api.Model
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string Nick { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public bool? Activo { get; set; }
        public string RutaImagen { get; set; }
        public string Theme { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaMod { get; set; }
    }
}
