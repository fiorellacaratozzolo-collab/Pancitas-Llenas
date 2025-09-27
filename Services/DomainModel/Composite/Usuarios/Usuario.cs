using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuarios
{
    public class Usuario : Persona
    {
        public Guid IdUsuario { get; set; }

        public string Email { get; set; }

        public string Contraseña { get; set; }

        public string Cargo { get; set; }


    }
}
