using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class InventarioSucursal
    {
        public Guid IdInventarioSucursal { get; set; }
        public Guid IdInventario { get; set; }
        public Guid IdSucursal { get; set; }
        public int IdEstadoIS { get; set; } //Clave Foránea a EstadoISEnum

        public EstadoISEnum EstadoIS { get; set; } //Propiedad de navegación

        public virtual Inventario Inventario { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        public virtual EstadoISEnum EstadoISEnum { get; set; }
    }
}
