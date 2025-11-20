using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class OrdenDePedidoDTO
    {
        public Guid IdOrdenDePedido { get; set; }

        public DateTime FechaOp { get; set; }

        public int IdEstadoOp { get; set; }

        public decimal Total { get; set; }

        public Guid? IdSolicitudDePedidoOrigen { get; set; }

        public virtual EstadoOpenumDTO IdEstadoOpNavigation { get; set; } = null!;

        public virtual ICollection<OrdenDePedidoDetalleDTO> OrdenDePedidoDetalles { get; set; } = new List<OrdenDePedidoDetalleDTO>();
    }
}
