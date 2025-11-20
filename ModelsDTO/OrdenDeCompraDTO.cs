using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class OrdenDeCompraDTO
    {
        public Guid IdOrdenDeCompra { get; set; }

        public DateTime FechaOc { get; set; }

        public decimal Total { get; set; }

        public int IdEstadoOc { get; set; }

        // Permite rastrear la Orden de Pedido que originó esta Orden de Compra.
        public Guid? IdOrdenDePedidoOrigen { get; set; }

        // Campo para agrupar y filtrar la impresión
        public Guid IdProveedor { get; set; }
        public virtual ProveedorDTO IdProveedorNavigation { get; set; } = null!;

        public virtual EstadoOcenumDTO IdEstadoOcNavigation { get; set; } = null!;

        public virtual ICollection<OrdenDeCompraDetalleDTO> OrdenDeCompraDetalles { get; set; } = new List<OrdenDeCompraDetalleDTO>();
    }
}
