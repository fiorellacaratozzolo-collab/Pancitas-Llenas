using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Detalles
{
    public class OrdenDePedidoDetalle
    {
        public Guid IdOrdenDePedidoDetalle { get; set; }
        public Guid IdOrdenDePedido {  get; set; }

        public int Cantidad { get; set; }

        public decimal PesoNeto { get; set; }

        public decimal PrecioUnitario { get; set; }

        public string Unidad { get; set; }

        public decimal Subtotal { get; set; }
    }
}
