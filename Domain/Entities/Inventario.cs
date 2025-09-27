using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Inventario
    {
        public Guid IdInventario { get; set; }

        public List<Producto> Productos { get; set; }

        public int IdEstadoStock { get; set; } //Clave Foránea a EstadoStockEnum

        public EstadoStockEnum EstadoStock { get; set; } //Propiedad de navegación

        public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } //Relación con tabla intermedia

        public virtual ICollection<InventarioSucursal> InventarioSucursal { get; set; }

        public virtual EstadoStockEnum EstadoStockEnum { get; set; }

    }
}
