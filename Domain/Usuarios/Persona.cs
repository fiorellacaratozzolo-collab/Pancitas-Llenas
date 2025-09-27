using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuarios
{
    public class Persona
    {
        public Guid IdPersona { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int DNI { get; set; }
    }
}
