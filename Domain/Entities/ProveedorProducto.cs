using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProveedorProducto
    {
        public Guid IdProveedorProducto { get; set; }
        public Guid IdProducto { get; set; }
        public Guid IdProveedor { get; set; }

        public virtual Producto Producto { get; set; } //Relacion de tabla para saber a que entidad apuntar
        public virtual Proveedor Proveedor { get; set; } //Relacion de tabla para saber a que entidad apuntar

    }
}
