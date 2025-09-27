using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Proveedor
    {
        public Guid IdProveedor { get; set; }

        public string NombreProveedor { get; set; }

        public int CUIT { get; set; }

        public int Telefono { get; set; }

        public string Direccion { get; set; }

        public List<Producto> Productos { get; set; }

        public virtual ICollection<ProveedorProducto> ProveedorProductos { get; set; } //Relación con tabla intermedia
    }
}
