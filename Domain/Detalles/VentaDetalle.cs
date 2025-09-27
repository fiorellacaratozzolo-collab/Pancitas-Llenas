using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Detalles
{
    public class VentaDetalle
    {
        public Guid IdVentaDetalle { get; set; }     
        public Guid IdVenta { get; set; }

        public int Cantidad { get; set; }

        public decimal PesoNeto { get; set; }

        public string Unidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }

        public virtual Venta Venta { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
