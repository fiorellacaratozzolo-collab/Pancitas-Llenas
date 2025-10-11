using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public partial class StockPorSucursal
    {
        public Guid IdStockSucursal { get; set; }

        public Guid IdProducto { get; set; }
        public Guid IdSucursal { get; set; }

        public int StockActual { get; set; }
        public int StockDeseado { get; set; }

        public int IdEstadoStock { get; set; } // FK al estado del semáforo (1: Verde, 2: Amarillo, 3: Rojo)

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
        public virtual EstadoStockEnum IdEstadoStockNavigation { get; set; } = null!;
    }
}
