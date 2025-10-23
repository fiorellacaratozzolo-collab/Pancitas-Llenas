using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class TipoSucursalEnumDTO
    {
        public int IdTipoSucursal { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<SucursalDTO> Sucursals { get; set; } = new List<SucursalDTO>();
    }
}
