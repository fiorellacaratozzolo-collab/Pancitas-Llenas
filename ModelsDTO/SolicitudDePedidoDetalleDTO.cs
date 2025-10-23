using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class SolicitudDePedidoDetalleDTO
    {
        public Guid IdSolicitudDePedidoDetalle { get; set; }

        public Guid IdSolicitudDePedido { get; set; }

        public Guid IdProducto { get; set; }

        public string Cantidad { get; set; } = null!;

        public decimal PesoNeto { get; set; }

        public string Unidad { get; set; } = null!;

        public virtual ProductoDTO IdProductoNavigation { get; set; } = null!;

        public virtual SolicitudDePedidoDTO IdSolicitudDePedidoNavigation { get; set; } = null!;
    }
}
