using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class SolicitudDeTraspasoDeProductosDetalleDTO
    {
        public Guid IdSolicitudDeTraspasoDeProductosDetalle { get; set; }

        public Guid IdSolicitudDeTraspasoDeProductos { get; set; }

        public Guid IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PesoNeto { get; set; }

        public string Unidad { get; set; } = null!;

        public virtual ProductoDTO IdProductoNavigation { get; set; } = null!;

        public virtual SolicitudDeTraspasoDeProductoDTO IdSolicitudDeTraspasoDeProductosNavigation { get; set; } = null!;
    }
}
