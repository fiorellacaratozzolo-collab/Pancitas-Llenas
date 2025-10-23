using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class SucursalDTO
    {
        public Guid IdSucursal { get; set; }

        public string Direccion { get; set; } = null!;

        public string NombreSucursal { get; set; } = null!;

        public int? Telefono { get; set; }

        public int IdTipoSucursal { get; set; }

        public virtual TipoSucursalEnumDTO IdTipoSucursalNavigation { get; set; } = null!;

        public virtual ICollection<StockPorSucursalDTO> StockPorSucursals { get; set; } = new List<StockPorSucursalDTO>();
    }
}
