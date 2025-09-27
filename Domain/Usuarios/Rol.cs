using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Usuarios
{
    public class Rol
    {
        public Guid IdRol { get; set; }

        public string NombreRol { get; set; }

        public List<Permisos> Permisos { get; set; }
    }
}
