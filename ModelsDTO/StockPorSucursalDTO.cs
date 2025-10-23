using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class StockPorSucursalDTO
    {
        public Guid IdStockSucursal { get; set; }

        public Guid IdProducto { get; set; }

        public Guid IdSucursal { get; set; }

        public int StockActual { get; set; }

        public int StockDeseado { get; set; }

        public int IdEstadoStock { get; set; }

        public virtual EstadoStockEnumDTO IdEstadoStockNavigation { get; set; } = null!;

        public virtual ProductoDTO IdProductoNavigation { get; set; } = null!;

        public virtual SucursalDTO IdSucursalNavigation { get; set; } = null!;
    }

}
