using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Logging
{
    public class Bitácora
    {
        public int IdBitacora { get; set; }
        public DateTime Fecha { get; set; }

        // Guid? (con el signo de interrogación) significa que permite nulos, 
        // útil si el error pasa antes de que alguien inicie sesión
        public Guid? IdUsuario { get; set; }

        public string Mensaje { get; set; }
        public Criticidad Criticidad { get; set; }

        // Propiedad extra para la vista
        public string NombreUsuario { get; set; }
    }
}
