using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class TipoVentaEnumDTO
    {
        public int IdTipoVenta { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<VentumDTO> Venta { get; set; } = new List<VentumDTO>();
    }

}
