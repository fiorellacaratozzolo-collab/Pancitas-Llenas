using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    public class ProveedorProductoInfo
    {
        public Guid IdProveedor { get; set; }

        // Es decimal (no acepta nulos) porque manejamos el posible nulo del origen
        public decimal PrecioNeto { get; set; }
    }

}
