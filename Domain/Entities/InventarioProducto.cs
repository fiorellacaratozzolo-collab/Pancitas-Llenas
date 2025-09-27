using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InventarioProducto
    {
        public Guid IdInventarioProducto { get; set; }
        public Guid IdInventario { get; set; }  //Clave Foránea a Inventario
        public Guid IdProducto { get; set; } //Clave Foránea a Producto

        public virtual Inventario Inventario { get; set; } //Relacion de tabla para saber a que entidad apuntar
        public virtual Producto Producto { get; set; } //Relacion de tabla para saber a que entidad apuntar

    }
}
