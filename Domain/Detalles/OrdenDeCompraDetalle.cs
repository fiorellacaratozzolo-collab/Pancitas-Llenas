using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Detalles
{
    public class OrdenDeCompraDetalle
    {
        public Guid IdOrdenDeCompraDetalle { get; set; }
        public Guid IdOrdenDeCompra {  get; set; }

        public int Cantidad {  get; set; }

        public decimal PesoNeto { get; set; }

        public decimal PrecioUnitario {  get; set; }

        public string Unidad { get; set; }

        public decimal Subtotal { get; set; }

        public virtual OrdenDeCompra OrdenDeCompra { get; set;}

    }
}
