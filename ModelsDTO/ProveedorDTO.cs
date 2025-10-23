using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class ProveedorDTO
    {
        public Guid IdProveedor { get; set; }

        public string NombreProveedor { get; set; } = null!;

        public int Cuit { get; set; }

        public int? Telefono { get; set; }

        public string? Direccion { get; set; }

        public virtual ICollection<ProveedorProductoDTO> ProveedorProductos { get; set; } = new List<ProveedorProductoDTO>();
    }
}
