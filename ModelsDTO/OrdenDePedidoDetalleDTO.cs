using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class OrdenDePedidoDetalleDTO
    {
        public Guid IdOrdenDePedidoDetalle { get; set; }

        public Guid IdOrdenDePedido { get; set; }

        public int Cantidad { get; set; }

        public decimal PesoNeto { get; set; }

        public decimal PrecioUnitario { get; set; }

        public string Unidad { get; set; } = null!;

        public decimal Subtotal { get; set; }

        public virtual OrdenDePedidoDTO IdOrdenDePedidoNavigation { get; set; } = null!;
    }

}
