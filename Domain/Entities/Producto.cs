using Domain.Detalles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Producto
    {
        public Guid IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public string Marca { get; set; }

        public decimal PesoNeto { get; set; }

        public string Unidad { get; set; }

        public decimal PrecioNeto { get; set; }

        public string Descripcion { get; set; }

        public virtual ICollection<ProveedorProducto> ProveedorProductos { get; set; } //Relación con tabla intermedia

        public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } //Relación con tabla intermedia

        public virtual ICollection<SolicitudDePedidoDetalle> SolicitudDePedidoDetalle { get; set; } 

        public virtual ICollection<SolicitudDeTraspasoDeProductosDetalle> SolicitudDeTraspasoDeProductosDetalles { get; set; }

    }
}
