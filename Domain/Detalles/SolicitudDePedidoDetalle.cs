using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Detalles
{
    public class SolicitudDePedidoDetalle
    {
        public Guid IdSolicitudDePedidoDetalle { get; set; }
        public Guid IdSolicitudDePedido { get; set; }
        public Guid IdProducto { get; set; }
        public string Cantidad { get; set; }
        public decimal PesoNeto { get; set; }
        public string Unidad { get; set; }


        public virtual Producto Producto { get; set; }
        public virtual SolicitudDePedido SolicitudDePedido { get; set; }

    }
    

}
