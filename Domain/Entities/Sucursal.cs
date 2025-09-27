using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sucursal
    {
        public Guid IdSucursal { get; set; }

        public string Direccion { get; set; }

        public string NombreSucursal { get; set; }

        public int Telefono { get; set; }

        public int IdTipoSucursal { get; set; } //Clave Foránea a TipoSucursalEnum

        public TipoSucursalEnum TipoSucursal { get; set; } //Propiedad de navegación

        public List<Inventario> Inventario { get; set; }

        public virtual ICollection<InventarioSucursal> InventarioSucursal { get; set;}

        public virtual TipoSucursalEnum TipoSucursalEnum { get; set; }
    }
}
