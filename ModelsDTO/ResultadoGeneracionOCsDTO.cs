using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public class ResultadoGeneracionOCsDTO
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        // Diccionario: Key=IdProveedor, Value=IdOrdenDeCompra
        public Dictionary<Guid, Guid> OrdenesCreadas { get; set; } = new Dictionary<Guid, Guid>();
    }
}
