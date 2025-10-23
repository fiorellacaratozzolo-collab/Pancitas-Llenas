using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class VentaDetalleDTO
    {
        public Guid IdVentaDetalle { get; set; }

        public Guid IdVenta { get; set; }

        public Guid IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PesoNeto { get; set; }

        public string Unidad { get; set; } = null!;

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }

        public virtual ProductoDTO IdProductoNavigation { get; set; } = null!;

        public virtual VentumDTO IdVentaNavigation { get; set; } = null!;
    }
}
