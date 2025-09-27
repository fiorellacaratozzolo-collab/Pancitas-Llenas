using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Detalles
{
    public class SolicitudDeTraspasoDeProductosDetalle
    {
        public Guid IdSolicitudDeTraspasoDeProductosDetalle { get; set; }
        public Guid IdSolicitudDeTraspasoDeProductos {  get; set; }
        public Guid IdProducto {  get; set; }
        public int Cantidad { get; set; }

        public decimal PesoNeto { get; set; }

        public string Unidad { get; set; }

        public virtual SolicitudDeTraspasoDeProductos SolicitudDeTraspasoDeProductos { get; set; }
        public virtual Producto Producto { get; set; }

    }
}
